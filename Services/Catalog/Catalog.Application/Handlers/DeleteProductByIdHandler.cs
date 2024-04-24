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
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdQuery, ResponseViewModel>
    {
        private readonly IProductManager _productManager;
        public DeleteProductByIdHandler(IProductManager productManager)
        {
            _productManager= productManager;
        }

        public async Task<ResponseViewModel> Handle(DeleteProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _productManager.DeleteProductById(request.Id);
              
                return new SuccessResponseViewModel()
                {
                    Data = res
                };
            }
            catch(Exception ex)
            {
                return new FailResponseViewModel("Internal server Error", HttpStatusCode.InternalServerError)
                {
                    Data = ex.Message,
                };
            }
        }
    }
}
