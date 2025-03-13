using Identity.Library.Configurations;
using Identity.Library.Contexts;
using Identity.Library.Manager.Interface;
using Identity.Service.Repository.Contexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var deletableClients = _confcontext.Clients.Where(x => clients.Select(x => x.ClientName).Contains(x.ClientName));
            if (deletableClients.Any())
                _confcontext.Clients.RemoveRange(deletableClients);
            await _confcontext.Clients.AddRangeAsync(clients);


            // IDENTITY RESOURCES
            var idResources = IdentityResourceConfiguration.GetIdentityResources().Select(x => x.ToEntity());
            var deletableIdResources = _confcontext.IdentityResources.Where(x => idResources.Select(x => x.Name).Contains(x.Name));
            if (deletableIdResources.Any())
                _confcontext.IdentityResources.RemoveRange(deletableIdResources);
            await _confcontext.IdentityResources.AddRangeAsync(idResources);


            // API SCOPES
            var scopes = ScopeConfiguration.GetScopes().Select(x => x.ToEntity());
            var deletableScopes = _confcontext.ApiScopes.Where(x => scopes.Select(x => x.Name).Contains(x.Name));
            if (deletableScopes.Any())
                _confcontext.ApiScopes.RemoveRange(deletableScopes);
            await _confcontext.ApiScopes.AddRangeAsync(scopes);


            // API RESOURCES
            var apiResources = ApiResourceConfiguration.GetApiResources().Select(x => x.ToEntity());
            var deletableApiResources = _confcontext.ApiResources.Where(x => apiResources.Select(x => x.Name).Contains(x.Name));
            if (deletableApiResources.Any())
                _confcontext.ApiResources.RemoveRange(deletableApiResources);
            await _confcontext.ApiResources.AddRangeAsync(apiResources);


            return await _confcontext.SaveChangesAsync() > 0;
        }
    }
}
