using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Library.Configurations
{
    public static class ClientConfiguration
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client_local",
                    ClientSecrets = {
                        new Secret("secret_local".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = { "api1" }       // scopes that client has access to
                },

                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after login
                    RedirectUris = {
                        "https://localhost:5301/home/redirecturi"
                    },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = {
                        "https://localhost:5301/home/logoutredirecturi"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },

                
                // Postman client
                new Client
                {
                    ClientId = "postman_client",
                    ClientSecrets = {
                        new Secret("postman_secret".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    
                    // where to redirect to after login
                    //RedirectUris = {
                    //    "http://localhost:8080/"
                    //},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },

                //LOCAL CLIENT
                new Client
                {
                    ClientId = "vue id",
                    ClientSecrets = {
                        new Secret("vue secret".Sha256())
                    },

                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = {
                        "http://localhost:8080/authcallback",
                        "http://localhost:8080/callback.html"
                    },

                    PostLogoutRedirectUris = {
                        "http://localhost:8080/authcallbacksignout",
                        "https://localhost:8080/authcallbacksignout"
                    },

                    RequirePkce = true,

                    AllowOfflineAccess = true,

                    AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess
                    },

                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysSendClientClaims = true,
                    AccessTokenLifetime = 3600,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysIncludeUserClaimsInIdToken = true
                },
                new Client
                {
                    ClientId = "angular-id",
                    ClientSecrets = {
                        new Secret("angular-secret".Sha256())
                    },

                    //RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = {
                        "http://localhost:4200/authcallback",
                        "https://localhost:4200/authcallback",
                        "http://localhost:4200/auth/callback",
                        "https://localhost:4200/auth/callback"
                    },

                    PostLogoutRedirectUris = {
                        "http://localhost:4200/authcallbacksignout",
                        "https://localhost:4200/authcallbacksignout",
                        "http://localhost:4200/auth/callbacksignout",
                        "https://localhost:4200/auth/callbacksignout",
                    },

                    RequirePkce = true,

                    AllowOfflineAccess = true,

                    AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess
                    },

                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysSendClientClaims = true,
                    AccessTokenLifetime = 3600,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysIncludeUserClaimsInIdToken = true
                },
                new Client
                {
                    ClientId = "test-id",
                    ClientSecrets = {
                        new Secret("test".Sha256())
                    },

                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowRememberConsent = false,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    RedirectUris = {
                        "http://localhost:4200/authcallback",
                        "https://localhost:4200/authcallback",
                        "http://localhost:4200/auth/callback",
                        "https://localhost:4200/auth/callback"
                    },

                    PostLogoutRedirectUris = {
                        "http://localhost:4200/authcallbacksignout",
                        "https://localhost:4200/authcallbacksignout",
                        "http://localhost:4200/auth/callbacksignout",
                        "https://localhost:4200/auth/callbacksignout",
                    },

                    RequirePkce = true,

                    AllowOfflineAccess = true,

                    AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "IdentityServerApi"
                    },

                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysSendClientClaims = true,
                    AccessTokenLifetime = 3600,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysIncludeUserClaimsInIdToken = true
                },
            };
        }

    }
}
