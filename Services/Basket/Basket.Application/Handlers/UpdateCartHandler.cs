using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Library.Model.Entities;
using Basket.Repository.Manager.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, ShoppingCart>
    {
        public readonly IBasketManager _basketManager;
        private readonly DiscountGrpcService _discountGrpcService;
        public UpdateCartHandler(IBasketManager basketManager, DiscountGrpcService discountGrpcService)
        {
            _basketManager= basketManager;
            _discountGrpcService= discountGrpcService;
        }
        public async Task<ShoppingCart> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            decimal totalPrice = 0;
            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
                totalPrice += (item.Quantity * item.Price);
            }
            var res = await _basketManager.UpdateBasket(new ShoppingCart()
            {
                Name=request.Name,
                Items=request.Items,
                TotalPrice=totalPrice
            });

            return res;          
        }
    }
}
