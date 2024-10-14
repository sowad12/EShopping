using AutoMapper;
using Discount.Application.Commands;
using Discount.Grpc.Protos;
using Discount.Library.Model.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Mappers
{
    public class DiscountMappingProfile:Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<CreateDiscountCommand, Coupon>();
            CreateMap<UpdateDiscountCommand, Coupon>();
            CreateMap<Coupon, CouponModel>();
        }
    }
}
