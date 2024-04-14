﻿using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Library.Model.ViewModel;
using Catalog.Repository.Manager.Interface;
using MediatR;


namespace Catalog.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, bool>
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
            _typeManager= typeManager;
        }
        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var ReqProductData = ProductMapper.Mapper.Map<ProductViewModel>(request);
            var ReqProductBrandData = ProductMapper.Mapper.Map<ProductBrandViewModel>(request.Brands);
            var ReqProductTypeData = ProductMapper.Mapper.Map<ProductTypeViewModel>(request.Types);
            if(ReqProductBrandData is null)
            {
                throw new ApplicationException("Product Brand Name is Missing");
            }        
            if (ReqProductTypeData is null)
            {
                throw new ApplicationException("Product Type Name is Missing");
            }
            
            if (ReqProductData is null)
            {
                throw new ApplicationException("There is an issue with mapping while creating new product");
            }
            var ProductBrandResId = await _brandManager.InsertProductBrand(ReqProductBrandData);
            var ProductTypeResId = await _typeManager.InsertProductType(ReqProductTypeData);

            ReqProductData.ProductBrandId = ProductBrandResId;
            ReqProductData.ProductTypeId = ProductTypeResId;

            var res=await _productManager.CreateOrUpdateProduct(ReqProductData);

            return res;
           
        }
    }
}
