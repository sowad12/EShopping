using AutoMapper;
using Discount.Application.Commands;
using Discount.Grpc.Protos;
using Discount.Library.Model.Entites;
using Discount.Repository.Implementation;
using Discount.Repository.Interface;
using EShopping.Core.Exceptions;
using Grpc.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handlers
{
    public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
    {
        private readonly IDiscountManager _discountManager;
        private readonly IMapper _mapper;
        public UpdateDiscountHandler(IDiscountManager discountManager, IMapper mapper)
        {
            _discountManager = discountManager;
            _mapper = mapper;
        }
        public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            var res = await _discountManager.UpdateDiscount(coupon);
            if (res == false) throw new RpcException(new Status(StatusCode.Internal, "cannot update discount"));
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
