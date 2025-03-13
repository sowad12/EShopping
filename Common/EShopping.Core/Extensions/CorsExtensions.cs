
using EShopping.Core.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopping.Core.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CorsOptions>(configuration.GetSection(nameof(CorsOptions)));

            var corsOptions = configuration.GetSection(nameof(CorsOptions))
                .Get(typeof(CorsOptions)) as CorsOptions;

            services.AddCors(options =>
            {
                options.AddPolicy(corsOptions.PolicyName, policy =>
                {
                    if (corsOptions != null)
                    {
                        policy.WithOrigins(corsOptions.Origins);
                    }
                    else
                    {
                        policy.AllowAnyOrigin();
                    }
                    policy.AllowAnyHeader().AllowAnyMethod();
                    policy.AllowCredentials().SetIsOriginAllowed((host) => true);
                });
            });
            return services;
        }

        public static IApplicationBuilder UseCorsServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            var corsOptions = configuration.GetSection(nameof(CorsOptions))
                .Get(typeof(CorsOptions)) as CorsOptions;

            app.UseCors(corsOptions.PolicyName);
            return app;
        }

    }
}
