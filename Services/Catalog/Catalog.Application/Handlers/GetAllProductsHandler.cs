using Azure.Core;
using Catalog.Application.Queries;
using Catalog.Library.Model.ViewModel;
using Catalog.Repository.Manager.Interface;
using EShopping.Utilities.Pagination;
using MediatR;


namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, PagedCollection<ProductViewModel>>
    {
        private readonly IProductManager _productManager;
        public GetAllProductsHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<PagedCollection<ProductViewModel>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllProductViewModel getAllProductViewModel = new GetAllProductViewModel();
                getAllProductViewModel.SearchQuery = query.SearchQuery;
                getAllProductViewModel.pagingOptions = query.pagingOptions;
                getAllProductViewModel.sortOptions = query.sortOptions;
                var data = await _productManager.GetAllProducts(getAllProductViewModel);
                return new PagedCollection<ProductViewModel>(data, data.Count(), query.pagingOptions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
