using EShopping.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Commands;
using Order.Application.Queries;

namespace Order.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrderController : ControllerBase
    {
        private IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create-order")]
        public async Task<IActionResult> CheckOutOrder([FromBody]CheckoutOrderCommand checkOutOrderCommand)
        {
            var result = await _mediator.Send(checkOutOrderCommand).AsCreated();
            return Ok(result);
        }
        [HttpGet("get-order-by-user-name")]
        public async Task<IActionResult> GetOrdersByUserName(string userName)
        {         
            var result = await _mediator.Send(new GetOrdersByUserNameQuery() { UserName=userName}).AsSuccess();
            return Ok(result);
        }
        [HttpPut("update-order")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand updateOrderCommand)
        {
            var result = await _mediator.Send(updateOrderCommand).AsUpdated();
            return Ok(result);
        }
        [HttpPost("delete-order")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand() { Id=id}).AsDeleted();
            return Ok(result);
        }
    }
}
