using Basket.Application.Queries;
using Basket.Application.Response;
using Basket.Repository.Manager.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers
{
    public class DeleteBasketByNameHandler : IRequestHandler<DeleteBasketByNameQuery, Unit>
    {
        public readonly IBasketManager _basketManager;
        public DeleteBasketByNameHandler(IBasketManager basketManager)
        {
            _basketManager=basketManager;
        }
        public async Task<Unit> Handle(DeleteBasketByNameQuery request, CancellationToken cancellationToken)
        {
            await _basketManager.DeleteBasket(request.Name);
            return Unit.Value;
        }
    }
}
