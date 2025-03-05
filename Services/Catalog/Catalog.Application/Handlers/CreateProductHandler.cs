using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Library.Model.Entities;
using Catalog.Library.Model.ViewModel;
using Catalog.Repository.Manager.Interface;
using EShopping.Core.Exceptions;
using EShopping.Core.ViewModels;
using MediatR;
using System.Drawing.Drawing2D;
using System.Net;
using System.Reflection;


namespace Catalog.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductManager _productManager;
        private readonly IBrandManager _brandManager;
        private readonly ITypeManager _typeManager;
        public CreateProductHandler(IProductManager productManager,
            IBrandManager brandManager,
            ITypeManager typeManager)
        {
            _productManager = productManager;
            _brandManager = brandManager;
            _typeManager = typeManager;
        }
        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            long ProductBrandId = 0, ProductTypeId = 0;
            try
            {
                var ReqProductData = ProductMapper.Mapper.Map<ProductViewModel>(request);
                var ReqProductBrandData = ProductMapper.Mapper.Map<ProductBrandViewModel>(request.Brands);
                var ReqProductTypeData = ProductMapper.Mapper.Map<ProductTypeViewModel>(request.Types);
                if (ReqProductBrandData is null)
                {
                    throw new CustomException("Product Brand Name is Missing.", System.Net.HttpStatusCode.NotFound);
                }
                if (ReqProductTypeData is null)
                {
                    throw new CustomException("Product Type Name is Missing.", System.Net.HttpStatusCode.NotFound);
                }

                if (ReqProductData is null)
                {
                    throw new CustomException("There is an issue with mapping while creating new product.", HttpStatusCode.InternalServerError);

                }
                var ProductBrandRes = await _brandManager.InsertProductBrand(ReqProductBrandData);
                var ProductTypeRes = await _typeManager.InsertProductType(ReqProductTypeData);

                if (ProductTypeRes.Data is null || ProductBrandRes.Data is null)
                {
                    throw new CustomException("Internal server error", HttpStatusCode.InternalServerError);

                }

                ReqProductData.ProductBrandId = ProductBrandRes.Data;
                ReqProductData.ProductTypeId = ProductTypeRes.Data;

                var res = await _productManager.CreateOrUpdateProduct(ReqProductData);
             
                return res;
            }
            catch (CustomException ex)
            {
                var ProductBrandDel = await _brandManager.DeleteProductBrandById(ProductBrandId);
                var ProductTypeDel = await _typeManager.DeleteProductTypeById(ProductTypeId);
                throw ex;
            }
            catch (Exception ex)
            {
                var ProductBrandDel = await _brandManager.DeleteProductBrandById(ProductBrandId);
                var ProductTypeDel = await _typeManager.DeleteProductTypeById(ProductTypeId);
                throw ex;

            }


        }
    }
}
