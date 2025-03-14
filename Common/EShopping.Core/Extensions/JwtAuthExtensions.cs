using EShopping.Core.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace EShopping.Core.Extensions
{
    public static class JwtAuthExtensions
    {

        static string _secretKey;
        static SymmetricSecurityKey _securityKey;
        static SigningCredentials _signingKey;

        public static AuthenticationBuilder AddJwtAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtAuthConfig = configuration.GetSection(nameof(JwtAuthOptions));

            _secretKey = jwtAuthConfig[nameof(JwtAuthOptions.SecretKey)];
            _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
            _signingKey = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

            services.Configure<JwtAuthOptions>(option =>
            {
                option.Issuer = jwtAuthConfig[nameof(JwtAuthOptions.Issuer)];
                option.Audience = jwtAuthConfig[nameof(JwtAuthOptions.Audience)];
                option.SecretKey = _secretKey;
                option.SigningCredentials = _signingKey;
            });
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = CreateTokenParameters(configuration);
                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "Yes");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        static TokenValidationParameters CreateTokenParameters(IConfiguration config)
        {
            var _jwtAuthConfig = config.GetSection(nameof(JwtAuthOptions));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtAuthConfig[nameof(JwtAuthOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = _jwtAuthConfig[nameof(JwtAuthOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = false,

                ClockSkew = TimeSpan.Zero,
                SaveSigninToken = true
            };
            return tokenValidationParameters;
        }
    }

}
