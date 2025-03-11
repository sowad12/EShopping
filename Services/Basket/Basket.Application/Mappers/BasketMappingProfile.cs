using AutoMapper;
using Basket.Application.Commands;
using Eshopping.AMQ.Events;

namespace Basket.Application.Mappers
{
    public class BasketMappingProfile:Profile
    {
        public BasketMappingProfile()
        {
          CreateMap<BasketCheckOutEvent, BasketCheckoutCommand>().ReverseMap();
        }
    }
}
