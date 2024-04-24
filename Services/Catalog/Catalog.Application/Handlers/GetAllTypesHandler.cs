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
    public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, ResponseViewModel>
    {
        private readonly ITypeManager _typeManager;
        public GetAllTypesHandler(ITypeManager typeManager)
        {
            _typeManager = typeManager;
        }
        public async Task<ResponseViewModel> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _typeManager.GetAllTypes();
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
