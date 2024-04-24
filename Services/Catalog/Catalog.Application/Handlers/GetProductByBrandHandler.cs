﻿using Catalog.Application.Queries;
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
    public class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuery, ResponseViewModel>
    {
        private readonly IBrandManager _brandManager;
        private readonly IProductManager _productManager;
        public GetProductByBrandHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<ResponseViewModel> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = _productManager.GetProductByBrand(request.Brandname);

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
