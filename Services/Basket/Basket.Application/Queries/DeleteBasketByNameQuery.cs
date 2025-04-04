﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Queries
{
    public class DeleteBasketByNameQuery:IRequest<bool>
    {
        public string Name { get; set; }
    }
}
