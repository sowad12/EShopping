
using Basket.Application.Commands;
using Basket.Application.Handlers;
using Basket.Repository.Manager.Implementation;
using Basket.Repository.Manager.Interface;
using EShopping.Core.Middleware;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Basket.Api.Extensions
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

            //Redis Settings
            services.AddHttpContextAccessor();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            services.AddAutoMapper(typeof(Startup));

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Startup)));
             //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            //services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyConfiguration).Assembly));
            //services.AddMediatR(cf => cf.RegisterServicesFromAssemblies(typeof(UploadImageHandler).Assembly));

             services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
             typeof(UpdateCartCommand).Assembly,
             typeof(UpdateCartHandler).Assembly
         ));

            // Options
            services.AddOptions();
         

            // Add Database Context
            //services.AddDatabaseContextService(configuration, logger);
            services.AddScoped<ISession>(provider =>
            provider
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext
                .Session);


            services.AddScoped<IBasketManager, BasketManager>();
          


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
