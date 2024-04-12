using Catalog.Library.Model.ViewModel;

namespace Catalog.Api.Manager.Interface
{
    public interface IBrandManager
    {
        Task<IEnumerable<ProductBrandViewModel>> GetAllBrands();
    }
}
