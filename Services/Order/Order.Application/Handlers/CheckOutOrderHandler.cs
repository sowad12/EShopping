using EShopping.Core.Exceptions;
using MediatR;
using Order.Application.Commands;
using Order.Application.Mappers;
using Order.Library.Context;
using Order.Library.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Handlers
{
    public class CheckOutOrderHandler : IRequestHandler<CheckoutOrderCommand, string>
    {
        private readonly ApplicationDbContext _context;
        public CheckOutOrderHandler(ApplicationDbContext context)
        {
            _context = context;
        }     
        public async Task<string> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newData=OrderMapper.Mapper.Map<CustomerOrder>(request);
                await _context.CustomerOrder.AddAsync(newData);
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    return "order checkout successfully";
                }
                throw new CustomException("internal server error", System.Net.HttpStatusCode.InternalServerError);
            }
            catch (Exception ex) { 
             
            }
            throw new NotImplementedException();
        }
    }
}
