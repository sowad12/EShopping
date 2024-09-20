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
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<ProductBrandViewModel>>
    {
        private readonly IBrandManager _brandManager;
        public GetAllBrandsHandler(IBrandManager brandManager)
        {
            _brandManager = brandManager;

        }

        public async Task<IEnumerable<ProductBrandViewModel>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _brandManager.GetAllBrands();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
           
        }
    }
}
