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
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, ResponseViewModel>
    {
        private readonly IBrandManager _brandManager;
        public GetAllBrandsHandler(IBrandManager brandManager)
        {
            _brandManager = brandManager;

        }

        public async Task<ResponseViewModel> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _brandManager.GetAllBrands();
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
