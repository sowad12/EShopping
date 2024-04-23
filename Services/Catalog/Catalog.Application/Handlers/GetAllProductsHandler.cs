using Catalog.Application.Queries;
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
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ResponseViewModel>
    {
        private readonly IProductManager _productManager;
        public GetAllProductsHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<ResponseViewModel> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var GetData = await _productManager.GetAllProducts();
                return new SuccessResponseViewModel()
                {
                    Data = GetData
                };
            }
            catch (Exception ex)
            {
                return new FailResponseViewModel("Internal server Error", HttpStatusCode.InternalServerError)
                {
                    Data = ex.Message,
                };
            }

        }
    }
}
