using EShopping.Core.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class DeleteProductByIdQuery:IRequest<ResponseViewModel>
    {

        public long Id {  get; set; }
        public DeleteProductByIdQuery(long Id)
        {
            this.Id=Id;
        }

    }
}
