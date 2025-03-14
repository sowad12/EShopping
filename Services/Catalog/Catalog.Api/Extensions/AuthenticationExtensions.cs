using EShopping.Core.Extensions;
using EShopping.Core.Infrastructure.Options;

namespace Catalog.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<IdentityServerOptions>(configuration.GetSection(nameof(IdentityServerOptions)));
            var identityServerOption = configuration.GetSection(nameof(IdentityServerOptions)).Get(typeof(IdentityServerOptions)) as IdentityServerOptions;
            if (identityServerOption is not null && identityServerOption.Enabled)
            {
                services.AddIdentityServerAuthenticationServices(identityServerOption);
            }
            else
            {
                services.AddJwtAuthenticationServices(configuration);
            }

            return services;
        }
    }

}
