using EShopping.Utilities.Pagination;
using EShopping.Utilities.Sort;
using MediatR;
using Order.Library.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Queries
{
    public class GetOrdersByUserNameQuery:IRequest<IEnumerable<OrderViewModel>>
    {
        public string UserName { get; set; }
    }
}
