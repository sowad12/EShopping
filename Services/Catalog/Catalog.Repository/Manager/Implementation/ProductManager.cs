

using Catalog.Library.Model.ViewModel;
using Catalog.Repository.Manager.Interface;
using EShopping.Core.Infrastructure.Interface;
using EShopping.Core.ViewModels;
using System.Net;


namespace Catalog.Repository.Manager.Implementation
{
    public class ProductManager : IProductManager, IBrandManager, ITypeManager
    {
        private readonly IDapperContext _dapper;
        public ProductManager(IDapperContext dapper)
        {
            _dapper = dapper;
        }




        public async Task<ProductViewModel> GetProductById(long Id)
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

        public async Task<ProductViewModel> GetProductByBrand(string Name)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<ProductViewModel>("PRODUCT_SELECT_BY_BRAND_NAME", new
                {
                    Name = Name
                });
                return result.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductViewModel> GetProductByName(string Name)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<ProductViewModel>("PRODUCT_SELECT_BY_PRODUCT_NAME", new
                {
                    Name = Name
                });
                return result.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductViewModel>> GetAllProducts(GetAllProductViewModel query)
        {
            try
            {

                string orderBy = "01";
                if (query.sortOptions is not null && query.sortOptions.OrderBy is not null)
                {
                    if (query.sortOptions.OrderBy.FirstOrDefault() == "name asc")
                        orderBy = "10";
                    if (query.sortOptions.OrderBy.FirstOrDefault() == "name desc")
                        orderBy = "11";

                    //if (query.sortOptions.OrderBy.FirstOrDefault() == "subscriberStatus asc")
                    //    orderBy = "20";
                    //if (query.sortOptions.OrderBy.FirstOrDefault() == "subscriberStatus desc")
                    //    orderBy = "21";
                }
                var result = await _dapper.StoredProcedureQueryAsync<ProductViewModel>("PRODUCT_SELECT_ALL", new
                {
                    
                    SearchQuery = !string.IsNullOrEmpty(query.SearchQuery) ? query.SearchQuery.ToLower().Trim() : "",
                    StartRow = query.pagingOptions is null ? 0 : query.pagingOptions.Offset,
                    EndRow = query.pagingOptions is null?0: query.pagingOptions.Limit,
                    OrderBy= orderBy
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
        public async Task<string> CreateOrUpdateProduct(ProductViewModel product)
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
                return product.Id is null ? "Product Created Successfully" : "Product Updated Successfully";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseViewModel> InsertProductBrand(ProductBrandViewModel productBrandViewModel)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<long>("PRODUCT_BRAND_INSERT", new
                {
                    Name = productBrandViewModel.Name,

                });
                return new SuccessResponseViewModel()
                {
                    Data = result.FirstOrDefault(),
                };
            }
            catch (Exception ex)
            {
                return new FailResponseViewModel("Internal server Error", HttpStatusCode.InternalServerError)
                {
                    Data = null,
                };
            }
        }

        public async Task<ResponseViewModel> InsertProductType(ProductTypeViewModel productTypeViewModel)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<long>("PRODUCT_TYPE_INSERT", new
                {
                    Name = productTypeViewModel.Name,

                });
                return new SuccessResponseViewModel()
                {
                    Data = result.FirstOrDefault(),
                };
            }
            catch (Exception ex)
            {
                return new FailResponseViewModel("Internal server Error", HttpStatusCode.InternalServerError)
                {
                    Data = null,
                };
            }
        }
        public async Task<ResponseViewModel> DeleteProductById(long Id)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<bool>("PRODUCT_DELETE_BY_ID", new
                {
                    Id = Id
                });
                return new SuccessResponseViewModel()
                {
                    Data = "Product Remove Successfully",
                };

            }
            catch (Exception ex)
            {
                return new FailResponseViewModel("Internal server Error", HttpStatusCode.InternalServerError)
                {
                    Data = ex.Message,
                };
            }
        }

        public async Task<bool> DeleteProductBrandById(long Id)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<bool>("PRODUCT_BRAND_DELETE_BY_ID", new
                {
                    Id = Id
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductTypeById(long Id)
        {
            try
            {
                var result = await _dapper.StoredProcedureQueryAsync<bool>("PRODUCT_TYPE_DELETE_BY_ID", new
                {
                    Id = Id
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
