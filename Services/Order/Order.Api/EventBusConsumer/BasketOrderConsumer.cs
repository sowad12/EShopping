using AutoMapper;
using Eshopping.AMQ.Events;
using MassTransit;
using MediatR;
using Order.Application.Commands;

namespace Order.Api.EventBusConsumer
{
    public class BasketOrderConsumer : IConsumer<BasketCheckOutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public BasketOrderConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<BasketCheckOutEvent> context)
        {
            try
            {
                var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
                var result = await _mediator.Send(command);
            }
            catch(Exception ex)
            {
                throw;
            }
            throw new NotImplementedException();
        }
    }
}
