using Catalog.Library.Model.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetProductByNameQuery:IRequest<IList<ProductViewModel>>
    {
        public string Name {  get; set; }
        public GetProductByNameQuery(string Name)
        {
            this.Name = Name;
        }
    }
}
