using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using EShopping.Core.Extensions;
using Grpc.Core;
using MediatR;

namespace Discount.Api.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;
        public DiscountService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetDiscountQuery() { ProductName = request.ProductName });
            return result;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new CreateDiscountCommand() { Amount = request.Coupon.Amount, Description = request.Coupon.Description, ProductName = request.Coupon.ProductName });
            return result;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new UpdateDiscountCommand() { Id = request.Coupon.Id, Amount = request.Coupon.Amount, Description = request.Coupon.Description, ProductName = request.Coupon.ProductName });
            return result;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DeleteDiscountCommand() {  ProductName = request.ProductName });
            var response = new DeleteDiscountResponse
            {
                Success = result
            };
            return response;

        }
    }
}
