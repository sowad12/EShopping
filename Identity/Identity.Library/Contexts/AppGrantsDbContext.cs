using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace Identity.Library.Contexts
{
    public class AppGrantsDbContext : PersistedGrantDbContext<AppGrantsDbContext>
    {
        public AppGrantsDbContext(DbContextOptions<AppGrantsDbContext> options, OperationalStoreOptions storeOptions)
         : base(options, storeOptions) { }
    }
}
