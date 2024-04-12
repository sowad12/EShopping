using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Library.Model.Entities
{
    [Table(nameof(ProductType))]
    public class ProductType:BaseEntity
    {
        public string Name { get; set; }
    }
}
