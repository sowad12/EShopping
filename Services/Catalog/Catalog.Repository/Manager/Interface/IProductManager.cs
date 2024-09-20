using Catalog.Library.Model.Entities;
using Catalog.Library.Model.ViewModel;
using EShopping.Core.ViewModels;

namespace Catalog.Repository.Manager.Interface
{
    public interface IProductManager
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProductById(long Id);
        Task<ProductViewModel> GetProductByName(string Name);
        Task<ProductViewModel> GetProductByBrand(string Name);
        Task<string> CreateOrUpdateProduct(ProductViewModel product);
        Task<ResponseViewModel> DeleteProductById(long Id);
    }
}
