using Basket.Application.Queries;
using Basket.Application.Response;
using Basket.Repository.Manager.Interface;
using MediatR;


namespace Basket.Application.Handlers
{
    public class DeleteBasketByNameHandler : IRequestHandler<DeleteBasketByNameQuery, bool>
    {
        public readonly IBasketManager _basketManager;
        public DeleteBasketByNameHandler(IBasketManager basketManager)
        {
            _basketManager=basketManager;
        }
        public async Task<bool> Handle(DeleteBasketByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await _basketManager.DeleteBasket(request.Name);
                return true;
            }
            catch (Exception ex) {
                throw;
            }        
        }
    }
}
