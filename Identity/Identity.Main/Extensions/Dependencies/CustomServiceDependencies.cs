
using Identity.Library.Manager.Implementation;
using Identity.Library.Manager.Interface;

using System.Reflection;

namespace Identity.Main.Extensions.Dependencies
{
    public static class CustomServiceDependencies
    {
        public static IServiceCollection AddCustomServiceDependencies(this IServiceCollection services, Assembly[] assemblies)
        {

            services.AddTransient<IClientManager, ClientManager>();
            services.AddTransient<ISystemManager, SystemManager>();
            //services.AddTransient<IEmailService, EmailService>();

            return services;

        }

    }
}
