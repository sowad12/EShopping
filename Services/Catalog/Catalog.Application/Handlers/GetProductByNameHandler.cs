using Catalog.Application.Queries;
using Catalog.Library.Model.ViewModel;
using Catalog.Repository.Manager.Interface;
using EShopping.Core.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, ProductViewModel>
    {
        private readonly IProductManager _productManager;
        public GetProductByNameHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<ProductViewModel> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _productManager.GetProductByName(request.Name);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
