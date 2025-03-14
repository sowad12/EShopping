using EShopping.Core.Infrastructure.Options;
using Identity.Library.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Identity.Main.Extensions.Dependencies
{
    public static class DatabaseDependencies
    {

        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDbContext<AppIdentityDbContext>
                (x => x.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppConfigurationDbContext>
                (x => x.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.Configure<AppOptions>(_configuration.GetSection(nameof(AppOptions)));

            // CHECK IF CONNECTION AVAILABLE
            var dbBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>().UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            using (var context = new AppIdentityDbContext(dbBuilder.Options))
            {
                try
                {
                    var pendingMigrations = context.Database.GetPendingMigrations();
                    if (pendingMigrations.Any())
                    {
                        context.Database.Migrate();
                    }
                    //var canConect = context.Database.CanConnect();
                    //Log.Error($"Identity DB Connected : {(canConect ? "yes" : "no")}");
                }
                catch (Exception) { throw; }
            }


            return services;
        }

    }
}
