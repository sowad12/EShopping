using EShopping.Core.Extensions;
using System.Reflection;

namespace Identity.Main.Extensions.Dependencies
{
    public static class AppDependencies
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDatabaseDependencies(_configuration);

            services.AddCustomServiceDependencies(new Assembly[] { Assembly.GetExecutingAssembly() });

            services.AddIdentityDependencies(_configuration);

            services.AddAuthentication();

            services.AddLocalApiAuthentication();

            //services.AddRabbitMQDependency(_configuration, Assembly.GetExecutingAssembly());

            //services.AddAmazonS3Services(_configuration);

            services.AddCorsServices(_configuration);

            return services;
        }
    }
}
