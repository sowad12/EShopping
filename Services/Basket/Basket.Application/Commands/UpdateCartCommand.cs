using Basket.Library.Model.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Commands
{
    public class UpdateCartCommand:IRequest<ShoppingCart>
    {
        public UpdateCartCommand()
        {
            Items = new List<ShoppingCartItem> { new ShoppingCartItem() };
        }
        public string Name {  get; set; }   
        public List<ShoppingCartItem> Items { get; set; }
    }
}
