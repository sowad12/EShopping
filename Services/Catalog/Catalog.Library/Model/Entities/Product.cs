using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Library.Model.Entities
{
    [Table(nameof(Product))]
    public class Product:BaseEntity
    {
       
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        [Required] public long ProductBrandId { get; set; }
        [ForeignKey(nameof(ProductBrandId))]
        public virtual ProductBrand ProductBrand { get; set; }
        [Required] public long ProductTypeId { get; set; }

        [ForeignKey(nameof(ProductTypeId))]
        public virtual ProductType ProductType { get; set; }
        public decimal Price { get; set; }
    }
}
