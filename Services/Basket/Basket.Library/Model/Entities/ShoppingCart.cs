using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Library.Model.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items=new List<ShoppingCartItem>(); 
        }
        public string Name { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
    }
}
