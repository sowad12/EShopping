using Identity.Library.Configurations;
using Identity.Library.Contexts;
using Identity.Library.Manager.Interface;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;


namespace Identity.Library.Manager.Implementation
{
    public class SystemManager : ISystemManager
    {
        private readonly AppIdentityDbContext _idcontext;
        private readonly AppConfigurationDbContext _confcontext;
        private readonly AppGrantsDbContext _grantcontext;


        public SystemManager(AppIdentityDbContext idcontext, AppConfigurationDbContext confcontext, AppGrantsDbContext grantcontext)
        {
            _idcontext = idcontext;
            _confcontext = confcontext;
            _grantcontext = grantcontext;
        }

        public async Task<int> Migrate()
        {
            var result = await _idcontext.Database.GetPendingMigrationsAsync();
            var result2 = await _confcontext.Database.GetPendingMigrationsAsync();
            var result3 = await _grantcontext.Database.GetPendingMigrationsAsync();
            await _idcontext.Database.MigrateAsync();
            await _confcontext.Database.MigrateAsync();
            await _grantcontext.Database.MigrateAsync();

            return result.Count() + result2.Count() + result3.Count();
        }


        public async Task<bool> SeedDefault()
        {
            var migrations = await _confcontext.Database.GetPendingMigrationsAsync();
            if (migrations.Any())
                await Migrate();

            // DELETE
            // CLIENTS : Only Seeded clients will be deleted
            var clients = ClientConfiguration.GetClients().Select(x => x.ToEntity());
            //var clients = ClientConfiguration.GetClients().Select(x => new Client
            //{
            //    ClientId = x.ClientId,
            //    Enabled = x.Enabled,
            //    RequireClientSecret = x.RequireClientSecret,
            //    RequirePkce = x.RequirePkce,
            //    AllowedGrantTypes = x.AllowedGrantTypes?.Select(g => new ClientGrantType { GrantType = g }).ToList(),
            //    ClientSecrets = x.ClientSecrets?.Select(s => new ClientSecret { Value = s.Value }).ToList(),
            //    AllowedScopes = x.AllowedScopes?.Select(s => new ClientScope { Scope = s }).ToList(),
            //    RedirectUris = x.RedirectUris?.Select(uri => new ClientRedirectUri { RedirectUri = uri }).ToList(),
            //    PostLogoutRedirectUris = x.PostLogoutRedirectUris?.Select(uri => new ClientPostLogoutRedirectUri { PostLogoutRedirectUri = uri }).ToList()
            //}).ToList();
            var deletableClients = _confcontext.Clients.Where(x => clients.Select(x => x.ClientName).Contains(x.ClientName));
            if (deletableClients.Any())
                _confcontext.Clients.RemoveRange(deletableClients);
            await _confcontext.Clients.AddRangeAsync(clients);


            // IDENTITY RESOURCES
            var idResources = IdentityResourceConfiguration.GetIdentityResources().Select(x => x.ToEntity());
            //var idResources = IdentityResourceConfiguration.GetIdentityResources().Select(x => new IdentityResource
            //{
            //    Name = x.Name,
            //    DisplayName = x.DisplayName,
            //    Description = x.Description,
            //    Enabled = x.Enabled,
            //    Required = x.Required,
            //    Emphasize = x.Emphasize,
            //    ShowInDiscoveryDocument = x.ShowInDiscoveryDocument,
            //    UserClaims = x.UserClaims?.Select(c => new IdentityResourceClaim { Type = c }).ToList()
            //}).ToList();

            var deletableIdResources = _confcontext.IdentityResources.Where(x => idResources.Select(x => x.Name).Contains(x.Name));
            if (deletableIdResources.Any())
                _confcontext.IdentityResources.RemoveRange(deletableIdResources);
            await _confcontext.IdentityResources.AddRangeAsync(idResources);


            // API SCOPES
            var scopes = ScopeConfiguration.GetScopes().Select(x => x.ToEntity());
            //var scopes = ScopeConfiguration.GetScopes().Select(x => new ApiScope
            //{
            //    Name = x.Name,
            //    DisplayName = x.DisplayName,
            //    Description = x.Description,
            //    Enabled = x.Enabled,
            //    Required = x.Required,
            //    Emphasize = x.Emphasize,
            //    ShowInDiscoveryDocument = x.ShowInDiscoveryDocument,
            //    UserClaims = x.UserClaims?.Select(c => new IdentityServer4.EntityFramework.Entities.ApiScopeClaim { Type = c }).ToList()
            //}).ToList();

            var deletableScopes = _confcontext.ApiScopes.Where(x => scopes.Select(x => x.Name).Contains(x.Name));
            if (deletableScopes.Any())
                _confcontext.ApiScopes.RemoveRange(deletableScopes);
            await _confcontext.ApiScopes.AddRangeAsync(scopes);


            // API RESOURCES
            var apiResources = ApiResourceConfiguration.GetApiResources().Select(x => x.ToEntity());
            //var apiResources = ApiResourceConfiguration.GetApiResources().Select(x => new ApiResource
            //{
            //    Name = x.Name,
            //    DisplayName = x.DisplayName,
            //    Description = x.Description,
            //    Enabled = x.Enabled,
            //    Scopes = x.Scopes?.Select(s => new ApiResourceScope { Scope = s }).ToList(),
            //    UserClaims = x.UserClaims?.Select(c => new ApiResourceClaim { Type = c }).ToList(),
            //    Secrets = x.ApiSecrets?.Select(s => new ApiResourceSecret
            //    {
            //        Value = s.Value,
            //        Type = s.Type,
            //        Expiration = s.Expiration,
            //        Description = s.Description
            //    }).ToList()
            //}).ToList();

            var deletableApiResources = _confcontext.ApiResources.Where(x => apiResources.Select(x => x.Name).Contains(x.Name));
            if (deletableApiResources.Any())
                _confcontext.ApiResources.RemoveRange(deletableApiResources);
            await _confcontext.ApiResources.AddRangeAsync(apiResources);


            return await _confcontext.SaveChangesAsync() > 0;
        }
    }
}
