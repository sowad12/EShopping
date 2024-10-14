using Discount.Application.Commands;
using Discount.Application.Handlers;
using Discount.Repository.Implementation;
using Discount.Repository.Interface;
using EShopping.Core.Middleware;

namespace Discount.Api.Extensions
{
    public static class DiscountDependencyConfiguration
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
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
           typeof(CreateDiscountCommand).Assembly,
           typeof(CreateDiscountHandler).Assembly));
            // Options
            services.AddOptions();
            services.AddGrpc();

            // Add Database Context
            services.AddDiscountDatabaseContextService(configuration, logger);
            services.AddScoped<ISession>(provider =>
            provider
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext
                .Session);


            services.AddScoped<IDiscountManager, DiscountManager>();



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
