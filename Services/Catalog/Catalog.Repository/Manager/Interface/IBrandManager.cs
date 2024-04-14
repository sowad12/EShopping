

using Catalog.Library.Model.ViewModel;
using EShopping.Core.ViewModels;

namespace Catalog.Repository.Manager.Interface

{
    public interface IBrandManager
    {
        Task<IEnumerable<ProductBrandViewModel>> GetAllBrands();
        Task<ResponseViewModel> InsertProductBrand(ProductBrandViewModel productBrandViewModel);
    }
}
