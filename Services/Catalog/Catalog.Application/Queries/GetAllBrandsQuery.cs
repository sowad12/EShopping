using Catalog.Library.Model.ViewModel;
using EShopping.Core.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetAllBrandsQuery:IRequest<IEnumerable<ProductBrandViewModel>>
    {
    }
}
