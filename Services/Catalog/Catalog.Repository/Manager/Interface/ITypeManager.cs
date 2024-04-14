using Catalog.Library.Model.ViewModel;

namespace Catalog.Repository.Manager.Interface
{
    public interface ITypeManager
    {
        Task<IEnumerable<ProductTypeViewModel>> GetAllTypes();
        Task<long> InsertProductType(ProductTypeViewModel productTypeViewModel);
    }
}
