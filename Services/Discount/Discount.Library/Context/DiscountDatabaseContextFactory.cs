using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Library.Context
{
    public class DiscountDatabaseContextFactory
    {
        public DiscountApplicationDbContext Create(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<DiscountApplicationDbContext>()
                .UseSqlServer(
                connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                    sqlOptions.MigrationsAssembly(typeof(DiscountApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
                })
                .ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));

            return new DiscountApplicationDbContext(builder.Options);
        }

    }
}
