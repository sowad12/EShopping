using Catalog.Library.Model.Entities;
using Catalog.Library.Model.ViewModel;

namespace Catalog.Api.Manager.Interface
{
    public interface IProductManager
    {
        Task<IEnumerable<ProductViewModel>> GetProducts();
        Task<ProductViewModel> GetProduct(long Id);
        Task<IEnumerable<ProductViewModel>> GetProductByName(string name);
        Task<IEnumerable<ProductViewModel>> GetProductByBrand(string name);
        Task<ProductViewModel> CreateProduct(ProductViewModel product);
        Task<bool> UpdateProduct(ProductViewModel product);
        Task<bool> DeleteProduct(string id);
    }
}
