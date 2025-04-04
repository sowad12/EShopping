﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Library.Model.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public long ProductId { get; set; }
        public string ImageFile { get; set; }
        public string ProductName { get; set; }
    }
}
