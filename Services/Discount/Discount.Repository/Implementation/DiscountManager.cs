using Discount.Library.Context;
using Discount.Library.Model.Entites;
using Discount.Repository.Interface;
using EShopping.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Discount.Repository.Implementation
{
    public class DiscountManager : IDiscountManager
    {
        private readonly DiscountApplicationDbContext _dbContext;
        public DiscountManager(DiscountApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var IsNameExist = await _dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == coupon.ProductName);
            if (IsNameExist is not null) throw new CustomException("coupon name already exist",System.Net.HttpStatusCode.BadRequest); 
            await _dbContext.Coupons.AddAsync(coupon);
            var res = await _dbContext.SaveChangesAsync();
            return res > 0;

        }

        public async Task<bool> DeleteDiscount(string ProductName)
        {
            try
            {
                var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == ProductName);
                if (coupon != null)
                {
                    _dbContext.Coupons.Remove(coupon);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Coupon> GetDiscount(string ProductName)
        {
            var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == ProductName);
            return coupon;

        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var existingCoupon = await _dbContext.Coupons.FirstOrDefaultAsync(x => x.Id == coupon.Id);
            if (existingCoupon != null)
            {
                existingCoupon.ProductName = coupon.ProductName;
                existingCoupon.Amount = coupon.Amount;  
                existingCoupon.Description = coupon.Description;
                
            }
            var res=await _dbContext.SaveChangesAsync();
            return res > 0;

        }
    }
}
