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
    public class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuery, ProductViewModel>
    {
        private readonly IBrandManager _brandManager;
        private readonly IProductManager _productManager;
        public GetProductByBrandHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<ProductViewModel> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _productManager.GetProductByBrand(request.Brandname);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
