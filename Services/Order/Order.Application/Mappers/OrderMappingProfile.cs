using AutoMapper;
using Order.Application.Commands;
using Order.Application.Queries;
using Order.Library.Model.Entities;
using Order.Library.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Mappers
{
    public class OrderMappingProfile:Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CheckoutOrderCommand, CustomerOrder>().ReverseMap();
            CreateMap<GetOrdersByUserNameQuery, OrderViewModel>().ReverseMap();
            CreateMap<UpdateOrderCommand, CustomerOrder>().ReverseMap();
            CreateMap<CustomerOrder, OrderViewModel>();
        }
    }
}
