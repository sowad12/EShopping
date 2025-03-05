using Discount.Library.Model.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Repository.Interface
{
    public interface IDiscountManager
    {
        Task<Coupon> GetDiscount(string ProductName);
        Task<bool>CreateDiscount(Coupon coupon);
        Task<bool>UpdateDiscount(Coupon coupon);
        Task<bool>DeleteDiscount(string ProductName);
    }
}
