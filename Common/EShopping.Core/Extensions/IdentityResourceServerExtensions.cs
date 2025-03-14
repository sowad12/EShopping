using EShopping.Core.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EShopping.Core.Extensions
{
    public static class IdentityResourceServerExtensions
    {
        public static IServiceCollection AddIdentityServerAuthenticationServices(this IServiceCollection services, IdentityServerOptions optionData)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = optionData.Authority;
                        options.Audience = "IdentityServerApi";
                        //options.TokenValidationParameters = new TokenValidationParameters
                        //{
                        //    ValidateAudience = false
                        //};
                    });

            return services;
        }

        public static IApplicationBuilder UseIdentityResourceServerDependenies(this IApplicationBuilder app, IConfiguration configuration)
        {
            return app;
        }

    }
}
