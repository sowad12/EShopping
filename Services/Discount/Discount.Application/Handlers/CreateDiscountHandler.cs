
using AutoMapper;
using Discount.Application.Commands;
using Discount.Grpc.Protos;
using Discount.Library.Model.Entites;
using Discount.Repository.Interface;
using EShopping.Core.Exceptions;
using MediatR;

namespace Discount.Application.Handlers
{
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly IDiscountManager _discountManager;
        private readonly IMapper _mapper;
        public CreateDiscountHandler(IDiscountManager discountManager, IMapper mapper)
        {
            _discountManager = discountManager;
            _mapper = mapper;
        }
        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {

            var coupon = _mapper.Map<Coupon>(request);
            var res = await _discountManager.CreateDiscount(coupon);
            if (res == false) throw new CustomException("cannot create discount", System.Net.HttpStatusCode.InternalServerError);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
