

using Catalog.Library.Model.Entities;
using Catalog.Library.Model.ViewModel;
using Catalog.Repository.Manager.Interface;
using EShopping.Core.Infrastructure.Interface;


namespace Catalog.Repository.Manager.Implementation
{
    public class ProductManager : IProductManager, IBrandManager, ITypeManager
    {
        private readonly IDapperContext _dapper;
        public ProductManager(IDapperContext dapper)
        {
            _dapper = dapper;
        }


        public async Task<bool> DeleteProduct(long Id)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<bool>("PRODUCT_DELETE_BY_ID", new
                {
                    Id = Id
                });
                return result.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductViewModel> GetProduct(long Id)
        {
            try
            {
                
                var result = await _dapper.StoredProcedureQueryAsync<ProductViewModel>("PRODUCT_SELECT_BY_ID", new
                {
                    Id = Id
                });
                return result.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductByBrand(string Name)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<ProductViewModel>("PRODUCT_SELECT_BY_BRAND_NAME", new
                {
                    Name = Name
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductByName(string Name)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<ProductViewModel>("PRODUCT_SELECT_BY_PRODUCT_NAME", new
                {
                    Name = Name
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<ProductViewModel>("PRODUCT_SELECT_ALL", new
                {

                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<IEnumerable<ProductBrandViewModel>> GetAllBrands()
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<ProductBrandViewModel>("PRODUCT_BRAND_SELECT_ALL", new
                {

                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductTypeViewModel>> GetAllTypes()
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<ProductTypeViewModel>("PRODUCT_TYPE_SELECT_ALL", new
                {

                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> CreateOrUpdateProduct(ProductViewModel product)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<bool>("PRODUCT_INSERT_OR_UPDATE", new
                {
                    Id = product.Id ?? null,
                    ProductBrandId = product.ProductBrandId,
                    ProductTypeId = product.ProductTypeId,
                    Name = product.Name,
                    Summary = product.Summary,
                    Description = product.Description,
                    ImageFile = product.ImageFile,
                    Price = product.Price

                });
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<long> InsertProductBrand(ProductBrandViewModel productBrandViewModel)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<long>("PRODUCT_BRAND_INSERT", new
                {             
                    Name= productBrandViewModel.Name,

                });
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<long> InsertProductType(ProductTypeViewModel productTypeViewModel)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<long>("PRODUCT_TYPE_INSERT", new
                {
                    Name = productTypeViewModel.Name,

                });
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
