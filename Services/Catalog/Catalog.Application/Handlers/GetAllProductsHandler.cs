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
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductViewModel>>
    {
        private readonly IProductManager _productManager;
        public GetAllProductsHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<IEnumerable<ProductViewModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _productManager.GetAllProducts();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
