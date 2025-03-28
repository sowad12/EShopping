using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Library.Model.Entities;
using EShopping.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BasketController : ControllerBase
    {
        private  IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("update-cart")]
        public async Task<IActionResult> UpdateCart([FromBody]UpdateCartCommand updateCartCommand)
        {
            var result = await _mediator.Send(updateCartCommand).AsUpdated();
            return Ok(result);
        }

        [HttpGet("get-cart")]
        public async Task<IActionResult> GetCart([FromQuery]GetBasketByNameQuery  getBasketByNameQuery)
        {
            var result = await _mediator.Send(getBasketByNameQuery).AsSuccess();
            return Ok(result);
        }
        [HttpPost("delete-cart")]
        public async Task<IActionResult> DeleteCart(DeleteBasketByNameQuery deleteBasketByNameQuery )
        {
            var result = await _mediator.Send(deleteBasketByNameQuery).AsDeleted();
            return Ok(result);
        }
        [HttpPost("basket-checkout")]
        public async Task<IActionResult> BasketCheckout([FromBody]BasketCheckoutCommand basketCheckout)
        {
            var result = await _mediator.Send(basketCheckout).AsCreated();        
            return Ok(result);
        }

    }
}
