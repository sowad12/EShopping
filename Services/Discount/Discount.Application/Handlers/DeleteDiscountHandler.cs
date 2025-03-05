using Discount.Application.Commands;
using Discount.Repository.Interface;
using MediatR;

namespace Discount.Application.Handlers
{
    public class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        public readonly IDiscountManager _discountManager;
        public DeleteDiscountHandler(IDiscountManager discountManager)
        {
            _discountManager=discountManager;
        }
        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            return await _discountManager.DeleteDiscount(request.ProductName);
           
        }
    }
}
