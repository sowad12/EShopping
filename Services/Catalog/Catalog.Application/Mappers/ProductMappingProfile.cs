using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Library.Model.Entities;
using Catalog.Library.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Mappers
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
            CreateMap<CreateProductCommand,ProductViewModel>().ReverseMap();
        }
    }
}
