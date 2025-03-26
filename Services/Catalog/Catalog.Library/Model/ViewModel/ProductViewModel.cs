using Catalog.Library.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Catalog.Library.Model.ViewModel
{
    public class ProductViewModel
    {
        public long? Id { get; set; }    
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public long ProductBrandId { get; set; }   
        public long ProductTypeId { get; set; }
        [JsonIgnore]
        public long TotalRow { get; set; }
        public decimal Price { get; set; }=decimal.Zero;
    }
}
