using Basket.Application.Queries;
using Basket.Application.Response;
using Basket.Repository.Manager.Interface;
using MediatR;


namespace Basket.Application.Handlers
{
    public class DeleteBasketByNameHandler : IRequestHandler<DeleteBasketByNameQuery, string>
    {
        public readonly IBasketManager _basketManager;
        public DeleteBasketByNameHandler(IBasketManager basketManager)
        {
            _basketManager=basketManager;
        }
        public async Task<string> Handle(DeleteBasketByNameQuery request, CancellationToken cancellationToken)
        {
            await _basketManager.DeleteBasket(request.Name);
            return "Delete success";
        }
    }
}
