using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Discount.Repository.Interface;
using EShopping.Core.Exceptions;
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
            if(res is  null) throw new CustomException("Discount with product not found",System.Net.HttpStatusCode.NotFound);

           var couponModel=new CouponModel()
           {
               Id=res.Id,
               Amount = (int)res.Amount,
               Description=res.Description,
               ProductName=res.ProductName,
           };
            return couponModel; 
        }
    }
}
