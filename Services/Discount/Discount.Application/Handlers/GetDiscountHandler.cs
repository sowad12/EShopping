using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Discount.Repository.Interface;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetDiscountHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        public readonly IDiscountManager _discountManager;
        public GetDiscountHandler(IDiscountManager discountManager)
        {
            _discountManager=discountManager;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var res=await _discountManager.GetDiscount(request.ProductName);
            //if(res is  null) throw new RpcException(new Status(StatusCode.NotFound,"Discount with product not found"));

           var couponModel=new CouponModel()
           {
               Id= res is null?0:res.Id,
               Amount = res is null ? 0 : (int)res.Amount,
               Description= res is null ? string.Empty : res.Description,
               ProductName=res is null?string.Empty:res.ProductName
           };
            return couponModel; 
        }
    }
}
