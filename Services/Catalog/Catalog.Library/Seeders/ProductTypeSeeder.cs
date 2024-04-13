using Catalog.Library.Context;
using Catalog.Library.Extensions;
using Catalog.Library.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Library.Seeders
{
    public class ProductTypeSeeder:ISeeder
    {
        public static List<ProductType> AllProductType = new List<ProductType>
        {
            new ProductType { Id = 1, Name= "Shoes"},
            new ProductType { Id = 2, Name= "Rackets" },
            new ProductType { Id = 3, Name= "Football" },
            new ProductType { Id = 4, Name= "Kit Bags" },
      
        };
        public void Seed(ApplicationDbContext context)
        {
            context.AddOrUpdateRange(AllProductType);
        }
    }
}

