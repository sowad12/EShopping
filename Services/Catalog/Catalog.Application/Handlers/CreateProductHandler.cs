using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Library.Model.ViewModel;
using Catalog.Repository.Manager.Interface;
using MediatR;


namespace Catalog.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IProductManager _productManager;
        public CreateProductHandler(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var ReqProductData=ProductMapper.Mapper.Map<ProductViewModel>(request);
            var ReqProductBrandData = ProductMapper.Mapper.Map<ProductBrandViewModel>(request.Brands);
            var ReqProductTypeData = ProductMapper.Mapper.Map<ProductTypeViewModel>(request.Types);
            if (ReqProductData is null)
            {
                throw new ApplicationException("There is an issue with mapping while creating new product");
            }
            var res=await _productManager.CreateOrUpdateProduct(ReqProductData);

            return res;
           
        }
    }
}
