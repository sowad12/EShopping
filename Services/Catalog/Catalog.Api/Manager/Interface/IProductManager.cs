using Catalog.Library.Model.Entities;
using Catalog.Library.Model.ViewModel;

namespace Catalog.Api.Manager.Interface
{
    public interface IProductManager
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProduct(long Id);
        Task<IEnumerable<ProductViewModel>> GetProductByName(string Name);
        Task<IEnumerable<ProductViewModel>> GetProductByBrand(string Name);
        Task<bool> CreateOrUpdate(ProductViewModel product);
       
        Task<bool> DeleteProduct(long Id);
    }
}
