using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Eshopping.AMQ.Events;
using EShopping.Core.Exceptions;
using MassTransit;
using MediatR;

namespace Basket.Application.Handlers
{
    public class BasketCheckoutHandler : IRequestHandler<BasketCheckoutCommand, string>
    {
        public readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        public BasketCheckoutHandler(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<string> Handle(BasketCheckoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var basket = await _mediator.Send(new GetBasketByNameQuery() { Name = request.UserName });
                if (basket == null)
                {
                    throw new CustomException("basket empty", System.Net.HttpStatusCode.NotFound);
                }
                var eventData = BasketMapper.Mapper.Map<BasketCheckOutEvent>(request);  
                await _publishEndpoint.Publish(eventData);
                var isDeleted=await _mediator.Send(new DeleteBasketByNameQuery() { Name=request.UserName});
                if (isDeleted)
                {
                    return "success";
                }
                throw new CustomException("internal server error", System.Net.HttpStatusCode.InternalServerError);

            }
            catch (Exception ex)
            {
                throw;
            }
            throw new NotImplementedException();
        }
    }
}
