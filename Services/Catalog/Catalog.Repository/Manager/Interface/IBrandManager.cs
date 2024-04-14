

using Catalog.Library.Model.ViewModel;

namespace Catalog.Repository.Manager.Interface

{
    public interface IBrandManager
    {
        Task<IEnumerable<ProductBrandViewModel>> GetAllBrands();
        Task<long> InsertProductBrand(ProductBrandViewModel productBrandViewModel);
    }
}
