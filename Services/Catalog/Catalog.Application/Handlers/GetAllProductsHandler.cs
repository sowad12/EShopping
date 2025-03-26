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
                getAllProductViewModel.BrandId = query.BrandId;
                getAllProductViewModel.TypeId = query.TypeId;   
                getAllProductViewModel.SearchQuery = query.SearchQuery;
                getAllProductViewModel.pagingOptions = query.pagingOptions;
                getAllProductViewModel.Orderby = query.Orderby;
                var data = await _productManager.GetAllProducts(getAllProductViewModel);
                var totalRow = data?.FirstOrDefault()?.TotalRow ?? 0;
                return new PagedCollection<ProductViewModel>(data, totalRow, query.pagingOptions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
