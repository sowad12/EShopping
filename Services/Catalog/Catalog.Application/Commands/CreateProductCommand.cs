using Catalog.Library.Model.Entities;
using Catalog.Library.Model.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commands
{
    public class CreateProductCommand:IRequest<bool>
    {
        public CreateProductCommand()
        {
            Brands = new ProductBrandViewModel();
            Types=new ProductTypeViewModel();
        }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public ProductBrandViewModel Brands { get; set; }
        public ProductTypeViewModel Types { get; set; }
    }
}
