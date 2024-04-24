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
    public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, ResponseViewModel>
    {
        private readonly IProductManager _productManager;
        public GetProductByNameHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<ResponseViewModel> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = _productManager.GetProductByName(request.Name);
                return new SuccessResponseViewModel()
                {
                    Data = res
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
