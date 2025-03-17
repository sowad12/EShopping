using EShopping.Utilities.Pagination;
using EShopping.Utilities.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Library.Model.ViewModel
{
    public class GetAllProductViewModel
    {
        public long? BrandId { get; set; }
        public long? TypeId { get; set; }
        public string SearchQuery { get; set; }
        public PagingOptions pagingOptions { get; set; }
        public SortOptions sortOptions { get; set; }
    }
}
