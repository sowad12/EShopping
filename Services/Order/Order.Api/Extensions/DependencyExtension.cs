
using Eshopping.AMQ.Common;
using EShopping.Core.Middleware;
using MassTransit;
using Order.Api.EventBusConsumer;
using Order.Api.Extensions;

namespace Order.Api.Extensions
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ILogger<Startup> logger = services
                           .BuildServiceProvider()
                           .GetRequiredService<ILogger<Startup>>();

            services.AddSwaggerService(configuration);
            services.AddMemoryCache();

            services.AddAutoMapper(typeof(Startup));

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Startup)));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            // Options
            services.AddOptions();
         

            // Add Database Context
            services.AddDatabaseContextService(configuration, logger);
            services.AddScoped<ISession>(provider =>
            provider
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext
                .Session);

            services.AddMassTransit(config =>
            {
                //Mark this as consumer
                config.AddConsumer<BasketOrderConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                    //provide the queue name with consumer settings
                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
                    {
                        c.ConfigureConsumer<BasketOrderConsumer>(ctx);
                    });
                    ////V2 endpoint will pick items from here 
                    //cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueueV2, c =>
                    //{
                    //    c.ConfigureConsumer<BasketOrderingConsumerV2>(ctx);
                    //});
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }

        public static IApplicationBuilder UseDependencies(
            this IApplicationBuilder app,
            IConfiguration configuration,
            //ILoggerFactory loggerFactory,

            IServiceProvider serviceProvider
        )
        {

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.UseSwaggerService();
         

            app.UseHttpsRedirection();

            app.UseRouting();

            
            app.UseAuthentication();

            app.UseAuthorization();
            return app;
        }
    }
}
