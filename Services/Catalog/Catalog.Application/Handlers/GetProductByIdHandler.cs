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
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductViewModel>
    {
        private readonly IProductManager _productManager;
        public GetProductByIdHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _productManager.GetProductById(request.Id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
