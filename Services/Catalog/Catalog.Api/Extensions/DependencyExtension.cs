//using Microsoft.AspNetCore.Mvc.Versioning;

//using EShopping.Core.Extensions;
using Catalog.Api.Manager.Interface;
using Catalog.Api.Manager.Implementation;
using Catalog.Api.Server.Main;

namespace Catalog.Api.Extensions
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
            //services.AddMediatR(typeof(CreateProductHandler).GetTypeInfo().Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Startup)));
            // Options
            services.AddOptions();
         

            // Add Database Context
            services.AddDatabaseContextService(configuration, logger);
            services.AddScoped<ISession>(provider =>
            provider
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext
                .Session);
           

            services.AddScoped<ISystemManager, SystemManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IBrandManager, ProductManager>();
            services.AddScoped<ITypeManager, ProductManager>();



        
            return services;
        }

        public static IApplicationBuilder UseDependencies(
            this IApplicationBuilder app,
            IConfiguration configuration,
            //ILoggerFactory loggerFactory,

            IServiceProvider serviceProvider
        )
        {
      

            app.UseSwaggerService();
         

            app.UseHttpsRedirection();

            app.UseRouting();

            
            app.UseAuthentication();

            app.UseAuthorization();
            return app;
        }
    }
}
