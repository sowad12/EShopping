using Catalog.Api.Manager.Interface;
using Catalog.Library.Model.ViewModel;
using EShopping.Core.Infrastructure.Interface;


namespace Catalog.Api.Manager.Implementation
{
    public class ProductManager : IProductManager, IBrandManager, ITypeManager
    {
        private readonly IDapperContext _dapper;
        public ProductManager(IDapperContext dapper)
        {
            _dapper = dapper;
        }
        public Task<ProductViewModel> CreateProduct(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

      

        public async Task<ProductViewModel> GetProduct(long Id)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<ProductViewModel>("PRODUCT_SELECT_BY_ID", new
                {
                    Id=Id

                });
                return result.FirstOrDefault();
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<ProductViewModel>> GetProductByBrand(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductViewModel>> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductViewModel>> GetProducts()
        {
            //try
            //{
            //    var result = await _dapper.StoredProcedureQueryAsync<int>("CLIENTSTORE_SELECT", new
            //    {
            //        //ClientInfoId = query.ClientInfoId ?? null,
            //        //MyStoreUrl = query.ShopUrl ?? null


            //    });

            //   // return result.FirstOrDefault();
            //    throw new NotImplementedException();
            //}
            //catch (Exception ex)
            //{
            //   //return new GetStartedViewModel();
            //}
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductBrandViewModel>> GetAllBrands()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductTypeViewModel>> GetAllTypes()
        {
            throw new NotImplementedException();
        }
    }
}
