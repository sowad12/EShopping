using Catalog.Library.Model.Entities;
using Catalog.Library.Model.ViewModel;
using EShopping.Core.ViewModels;

namespace Catalog.Repository.Manager.Interface
{
    public interface IProductManager
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProduct(long Id);
        Task<IEnumerable<ProductViewModel>> GetProductByName(string Name);
        Task<IEnumerable<ProductViewModel>> GetProductByBrand(string Name);
        Task<ResponseViewModel> CreateOrUpdateProduct(ProductViewModel product);
        Task<ResponseViewModel> DeleteProductById(long Id);
    }
}
