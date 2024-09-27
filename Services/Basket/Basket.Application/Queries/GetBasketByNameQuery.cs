using Basket.Library.Model.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Queries
{
    public class GetBasketByNameQuery:IRequest<ShoppingCart>
    {
        public string Name { get; set; }
    }
}
