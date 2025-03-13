
using Identity.Library.Contexts;
using Identity.Library.CustomInformation;
using Identity.Library.Domains.Entities;
using Identity.Main.Extensions.Configurations;
using Identity.Service.Repository.Contexts;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using System;
using System.Reflection;

namespace Identity.Main.Extensions.Dependencies
{
    public static class IdentityDependencies
    {
        public static IServiceCollection AddIdentityDependencies(this IServiceCollection services, IConfiguration _configuration)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            services.AddIdentity<User, Role>(x =>
            {
                x.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequireUppercase = true,
                    RequiredUniqueChars = 0,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequiredLength = 8
                };
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(24));

            services.AddCookieDependencies(_configuration);

            services.AddIdentityServer(x => x = new IdentityServerOptions
            {
                UserInteraction = new UserInteractionOptions
                {
                    LoginUrl = "/Account/Login",
                    LogoutUrl = "/Account/Logout",
                    ErrorUrl = "/Home/Error",
                    LoginReturnUrlParameter = "returnUrl"
                }
            })
            .AddAspNetIdentity<User>()
            .AddConfigurationStore<AppConfigurationDbContext>(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore<AppGrantsDbContext>(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));

                options.EnableTokenCleanup = true;
            })
            .AddSigningCredential(_configuration.GetCertificate())
            .AddProfileService<CustomUserProfileInformationAdder>();

            IdentityModelEventSource.ShowPII = true;

            return services;

        }

    }
}
