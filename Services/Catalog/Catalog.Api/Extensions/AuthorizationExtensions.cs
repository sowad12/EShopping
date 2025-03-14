namespace Catalog.Api.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
        {
        
            services.AddAuthorization();
            return services;
        }
    }

}
