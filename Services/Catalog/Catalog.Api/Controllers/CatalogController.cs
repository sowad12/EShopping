using Catalog.Application.Commands;
using Catalog.Application.Queries;
using EShopping.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CatalogController : ControllerBase
    {
        private IMediator _mediator;
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("get-all-movies")]
        public async Task<IActionResult> GetMovies()
        {   
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }
        //[HttpGet("get-movie/{Id}")]
        //public async Task<IActionResult> GetMovie([FromRoute] long Id)
        //{
        //    var result = await _mediator.Send(new GetProductByIdQuery() { Id = Id });
        //    return Ok(result);
        //}
        [HttpGet("get-movie")]
        public async Task<IActionResult> GetMovie([FromQuery] GetProductByIdQuery getProductByIdQuery)
        {
            var result = await _mediator.Send(getProductByIdQuery);
            return Ok(result);
        }

        [HttpPost("create-product")]   
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }
    }
}
