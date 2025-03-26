using Catalog.Library.Model.ViewModel;
using EShopping.Core.ViewModels;
using EShopping.Utilities.Pagination;
using EShopping.Utilities.Sort;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetAllProductsQuery:IRequest<PagedCollection<ProductViewModel>>
    {
        public long? BrandId { get; set; }
        public long? TypeId { get; set; }
        public string? SearchQuery { get; set; }
        public PagingOptions? pagingOptions { get; set; }
        //public SortOptions? sortOptions { get; set; }
        public string? Orderby { get; set; }
    }
}
