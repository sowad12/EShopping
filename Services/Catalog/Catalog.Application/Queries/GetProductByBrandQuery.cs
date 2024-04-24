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
    public class GetProductByBrandQuery:IRequest<ResponseViewModel>
    {
        public string Brandname { get; set; }
        public GetProductByBrandQuery(string Brandname)
        {
            this.Brandname = Brandname;
        }
    }
}
