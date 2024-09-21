using Catalog.Application.Queries;
using Catalog.Library.Model.ViewModel;
using Catalog.Repository.Manager.Interface;

using MediatR;


namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductViewModel>>
    {
        private readonly IProductManager _productManager;
        public GetAllProductsHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<IEnumerable<ProductViewModel>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllProductViewModel getAllProductViewModel=new GetAllProductViewModel();
                getAllProductViewModel.SearchQuery=query.SearchQuery;
                getAllProductViewModel.pagingOptions=query.pagingOptions;   
                getAllProductViewModel.sortOptions=query.sortOptions;
                var data = await _productManager.GetAllProducts(getAllProductViewModel);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
