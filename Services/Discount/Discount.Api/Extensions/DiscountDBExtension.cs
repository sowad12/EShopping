
using Discount.Library.Context;
using EShopping.Core.Infrastructure.Implementation;
using EShopping.Core.Infrastructure.Interface;
using EShopping.Core.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace Discount.Api.Extensions
{
    public static class DiscountDBExtension
    {
        public static IServiceCollection AddDiscountDatabaseContextService(this IServiceCollection services,
          IConfiguration configuration,
          ILogger<Startup> logger)
        {
            services.Configure<DatabaseOptions>(configuration.GetSection(nameof(DatabaseOptions)));

            DatabaseOptions databaseOptions = configuration.GetSection(nameof(DatabaseOptions))
                .Get(typeof(DatabaseOptions)) as DatabaseOptions;

            // Repository
            //services.AddScoped<IDapperContext, DapperContext>();
            services.AddScoped<DbContext, DiscountApplicationDbContext>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.Configure<AppOptions>(configuration.GetSection(nameof(AppOptions)));


            // Database Context
            services.AddDbContext<DiscountApplicationDbContext>(options =>
            {
                //var connectionBuilder = services.BuildServiceProvider().GetService<IDatabaseConnectionBuilderService>();
                //var connectionString = connectionBuilder.BuildAsync().Result;

                if (!string.IsNullOrEmpty(databaseOptions?.ConnectionString))
                {
                    options.UseSqlServer(databaseOptions.ConnectionString, sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                        sqlOptions.MigrationsAssembly(typeof(DiscountApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
                    })
                    .ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
                }
            });


            // CHECK IF CONNECTION AVAILABLE
            var dbBuilder = new DbContextOptionsBuilder<DiscountApplicationDbContext>().UseSqlServer(databaseOptions.ConnectionString);
            using (var context = new DiscountApplicationDbContext(dbBuilder.Options))
            {
                try
                {
                    var pendingMigrations = context.Database.GetPendingMigrations();
                    if (pendingMigrations.Any())
                    {
                        context.Database.Migrate();
                    }
                
                }
                catch (Exception e) { /*logger.LogError("Database migration issue occurred! Error Message: " + e.Message + "Stack Trace: " + e.StackTrace);*/ throw; }
            }

            return services;
        }
        public static IApplicationBuilder UseDatabaseContextService(this IApplicationBuilder app)
        {
            return app;
        }
    }
    
}
