
using EShopping.Core.Middleware;

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

            //services.AddAutoMapper(typeof(Startup));

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
