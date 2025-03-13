using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Identity.Main.Extensions.Dependencies
{
    public static class CookieDependencies
    {
        public static IServiceCollection AddCookieDependencies(this IServiceCollection services, IConfiguration _configuration)
        {
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "_cook_you_";
                config.LoginPath = "/account/login";
                config.LogoutPath = "/account/logout";

                //config = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions
                //{
                //    Cookie = new Microsoft.AspNetCore.Http.CookieBuilder
                //    {
                //        Name = "_cook_you_",
                //        Expiration = TimeSpan.FromSeconds(5)
                //    },
                //    LoginPath = "/account/login",
                //    LogoutPath = "/account/logout",

                //};
            });
            return services;
        }
    }
}
