using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Library.Model.Entities;
using Catalog.Library.Model.ViewModel;
using Catalog.Repository.Manager.Interface;
using EShopping.Core.ViewModels;
using MediatR;
using System.Drawing.Drawing2D;
using System.Net;
using System.Reflection;


namespace Catalog.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ResponseViewModel>
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
        public async Task<ResponseViewModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            long ProductBrandId=0, ProductTypeId=0;
            try
            {         
                var ReqProductData = ProductMapper.Mapper.Map<ProductViewModel>(request);
                var ReqProductBrandData = ProductMapper.Mapper.Map<ProductBrandViewModel>(request.Brands);
                var ReqProductTypeData = ProductMapper.Mapper.Map<ProductTypeViewModel>(request.Types);
                if (ReqProductBrandData is null)
                {                 
                    return new FailResponseViewModel("error", HttpStatusCode.NotFound)
                    {
                        Data = "Product Brand Name is Missing"
                    };
                }
                if (ReqProductTypeData is null)
                {                
                    return new FailResponseViewModel("error", HttpStatusCode.NotFound)
                    {
                        Data = "Product Type Name is Missing",
                    };
                }

                if (ReqProductData is null)
                {               
                    return new FailResponseViewModel("error", HttpStatusCode.InternalServerError)
                    {
                        Data = "There is an issue with mapping while creating new product"
                    };
                }
                var ProductBrandRes = await _brandManager.InsertProductBrand(ReqProductBrandData);
                var ProductTypeRes = await _typeManager.InsertProductType(ReqProductTypeData);

                if (ProductTypeRes.Data is null || ProductBrandRes.Data is null)
                {
                    return new FailResponseViewModel("error", HttpStatusCode.InternalServerError)
                    {
                        Data = null
                    };
                }

          
                ReqProductData.ProductBrandId = ProductBrandRes.Data;
                ReqProductData.ProductTypeId = ProductTypeRes.Data;

                var res = await _productManager.CreateOrUpdateProduct(ReqProductData);

                if (!res.Status.Message.Contains("success"))
                {                  
                        var ProductBrandDel=await _brandManager.DeleteProductBrandById(ProductBrandId);                                        
                        var ProductTypeDel = await _typeManager.DeleteProductTypeById(ProductTypeId);               
                }

                return res;
            }
            catch (Exception ex)
            {
                var ProductBrandDel = await _brandManager.DeleteProductBrandById(ProductBrandId);
                var ProductTypeDel = await _typeManager.DeleteProductTypeById(ProductTypeId);

                return new FailResponseViewModel("Internal server Error", HttpStatusCode.InternalServerError)
                {
                    Data = ex.Message,
                };
            }


        }
    }
}
