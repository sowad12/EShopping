

using Catalog.Library.Model.ViewModel;

namespace Catalog.Repository.Manager.Interface

{
    public interface IBrandManager
    {
        Task<IEnumerable<ProductBrandViewModel>> GetAllBrands();
        Task<bool> InsertProductBrand(ProductBrandViewModel productBrandViewModel);
    }
}
