using Catalog.Library.Model.ViewModel;
using EShopping.Core.ViewModels;

namespace Catalog.Repository.Manager.Interface
{
    public interface ITypeManager
    {
        Task<IEnumerable<ProductTypeViewModel>> GetAllTypes();
        Task<ResponseViewModel> InsertProductType(ProductTypeViewModel productTypeViewModel);
        Task<bool> DeleteProductTypeById(long Id);
    }
}
