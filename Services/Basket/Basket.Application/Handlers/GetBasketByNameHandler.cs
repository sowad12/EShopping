using Basket.Application.Queries;
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
    public class GetBasketByNameHandler : IRequestHandler<GetBasketByNameQuery, ShoppingCart>
    {
        public readonly IBasketManager _basketManager;
        public GetBasketByNameHandler(IBasketManager basketManager)
        {
            _basketManager= basketManager;
        }
        public async  Task<ShoppingCart> Handle(GetBasketByNameQuery request, CancellationToken cancellationToken)
        {
            var res=await _basketManager.GetBasket(request.Name);
            return res;
        }
    }
}
