using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace Identity.Library.Contexts
{
    public class AppConfigurationDbContext : ConfigurationDbContext<AppConfigurationDbContext>
    {
        public AppConfigurationDbContext(DbContextOptions<AppConfigurationDbContext> options, ConfigurationStoreOptions storeOptions)
         : base(options, storeOptions) { }
    }
}
