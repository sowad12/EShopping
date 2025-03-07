using EShopping.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Commands;
using Order.Library.Context;

namespace Order.Application.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, string>
    {
        private readonly ApplicationDbContext _context;
        public DeleteOrderHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _context.CustomerOrder.FirstOrDefaultAsync(x=>x.Id==request.Id);
                if (data == null)
                {
                    throw new CustomException("order not found",System.Net.HttpStatusCode.NotFound);
                }
                _context.CustomerOrder.Remove(data);
                var res= await _context.SaveChangesAsync();             
                if (res > 0)
                {
                    return "order deleted successfully";
                }
                throw new CustomException("internal server error", System.Net.HttpStatusCode.InternalServerError);
            }
            catch (Exception ex) {
                throw;
            }
        }
    }
}
