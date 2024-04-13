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
    public class ProductBrandSeeder:ISeeder
    {
        public static List<ProductBrand> AllProductBrand = new List<ProductBrand>
        {
            new ProductBrand { Id = 1, Name= "Adidas"},
            new ProductBrand { Id = 2, Name= "ASICS" },
            new ProductBrand { Id = 3, Name= "Victor" },
            new ProductBrand { Id = 4, Name= "Yonex" },
            new ProductBrand { Id = 5, Name= "Puma" },
            new ProductBrand { Id = 6, Name= "Nike" },
            new ProductBrand { Id = 7, Name= "Babolat" }        
        };
        public void Seed(ApplicationDbContext context)
        {
            context.AddOrUpdateRange(AllProductBrand);
        }
    }
}
