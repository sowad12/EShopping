using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Mappers;
using Order.Application.Queries;
using Order.Library.Context;
using Order.Library.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Handlers
{
    public class GetAllOrdersHandler : IRequestHandler<GetOrdersByUserNameQuery, IEnumerable<OrderViewModel>>
    {
        private readonly ApplicationDbContext _context;
   
        public GetAllOrdersHandler(ApplicationDbContext context)
        {
            _context = context;       
        }
        public async Task<IEnumerable<OrderViewModel>> Handle(GetOrdersByUserNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
              var res= await _context.CustomerOrder.AsNoTracking().Where(x=>x.UserName==request.UserName).ToListAsync(); 
              return OrderMapper.Mapper.Map<List<OrderViewModel>>(res);
            }
            catch (Exception ex) {
                throw;
            }     
        }
    }
}
