using Catalog.Library.Context;
using Catalog.Library.Extensions;
using Catalog.Library.Model.Entities;
using Catalog.Library.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Library.Seeders
{
    public class ProductSeeder:ISeeder
    {
        public static List<Product> AllProducts = new List<Product>
        {
           new Product
            {
                Id = 1,
                Name = "Adidas Quick Force Indoor Badminton Shoes",
                Description = "Designed for professional as well as amateur badminton players. These indoor shoes are crafted with synthetic upper that provides natural fit, while the EVA midsole provides lightweight cushioning. The shoes can be used for Badminton and Squash",
                Price = 3500,
                ImageFile = "images/products/adidas_shoe-1.png",
                Summary = "Adidas Quick Force Indoor Badminton Shoes",
                ProductBrandId=1,
                ProductTypeId=1

            },
            new Product
            {
                Id = 2,
                Name = "Nike Court Vision Low Sneakers",
                Description = "These Nike Court Vision Low Sneakers are designed with durable leather that is comfortable and supportive. The rubber sole provides traction on a variety of surfaces, making them ideal for both indoor and outdoor activities.",
                Price = 3000,
                ImageFile = "images/products/nike_sneakers-1.png",
                Summary = "Nike Court Vision Low Sneakers",
                ProductBrandId=6,
                ProductTypeId=1

            },

        };

        public void Seed(ApplicationDbContext context)
        {
            context.AddOrUpdateRange(AllProducts);
        }
    };
  
}

