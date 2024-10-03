using Discount.Library.Model.Entites;
using Discount.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Repository.Implementation
{
    public class DiscountManager : IDiscountManager
    {
        public Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDiscount(string ProductName)
        {
            throw new NotImplementedException();
        }

        public Task<Coupon> GetDiscount(string ProductName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
