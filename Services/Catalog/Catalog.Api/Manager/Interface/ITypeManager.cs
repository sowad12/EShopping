using Catalog.Library.Model.ViewModel;

namespace Catalog.Api.Manager.Interface
{
    public interface ITypeManager
    {
        Task<IEnumerable<ProductTypeViewModel>> GetAllTypes();
    }
}
