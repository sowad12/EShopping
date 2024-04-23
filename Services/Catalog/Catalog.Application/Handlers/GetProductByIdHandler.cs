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
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ResponseViewModel>
    {
        private readonly IProductManager _productManager;
        public GetProductByIdHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<ResponseViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var GetData = await _productManager.GetProductById(request.Id);
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
