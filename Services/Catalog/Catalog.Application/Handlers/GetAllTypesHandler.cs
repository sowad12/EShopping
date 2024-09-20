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
    public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IEnumerable<ProductTypeViewModel>>
    {
        private readonly ITypeManager _typeManager;
        public GetAllTypesHandler(ITypeManager typeManager)
        {
            _typeManager = typeManager;
        }
        public async Task<IEnumerable<ProductTypeViewModel>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _typeManager.GetAllTypes();
                return res;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
