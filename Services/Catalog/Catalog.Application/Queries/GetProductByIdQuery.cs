﻿using Catalog.Library.Model.ViewModel;
using EShopping.Core.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetProductByIdQuery:IRequest<ProductViewModel>
    {
        public long Id { get; set; }
        //public GetProductByIdQuery(long Id)
        //{
        //    this.Id = Id;
        //}
    }
}
