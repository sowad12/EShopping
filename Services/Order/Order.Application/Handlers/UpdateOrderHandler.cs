using EShopping.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, string>
    {
        private readonly ApplicationDbContext _context;
        public UpdateOrderHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _context.CustomerOrder.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (data == null)
                {
                    throw new CustomException("order not found", System.Net.HttpStatusCode.NotFound);
                }
                OrderMapper.Mapper.Map(request, data);
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    return "order updated successfully";
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
