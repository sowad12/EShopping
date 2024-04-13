using Catalog.Library.Context;
using Catalog.Library.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Library.Seeders
{
    public class MainSeeder
    {
        public int Seed(ApplicationDbContext context, ILogger logger)
        {
            try
            {
                var seedObjects = new List<ISeeder>()
                {
                    new ProductBrandSeeder(),
                    new ProductTypeSeeder(),
                    new ProductSeeder(),              
                };
                var result = SeederExtensions.IdentitySeedWithTransaction(context, seedObjects);
                logger?.LogWarning("SEED SUCCESS " + result);
                return result;
            }
            catch (Exception ex)
            {
                logger?.LogError("SEED ERROR " + ex.Message);
                throw;
            }
        }
    }
}
