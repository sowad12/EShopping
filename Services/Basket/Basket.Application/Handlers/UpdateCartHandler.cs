using Basket.Application.Commands;
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
        public UpdateCartHandler(IBasketManager basketManager)
        {
            _basketManager= basketManager;
        }
        public async Task<ShoppingCart> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var res = await _basketManager.UpdateBasket(new ShoppingCart()
            {
                Name=request.Name,
                Items=request.Items
            });
            return res;          
        }
    }
}
