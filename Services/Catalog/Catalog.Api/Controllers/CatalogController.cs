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
        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetProducts()
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
        [HttpGet("get-product")]
        public async Task<IActionResult> GetProduct([FromQuery] GetProductByIdQuery getProductByIdQuery)
        {
            var result = await _mediator.Send(getProductByIdQuery);
            return Ok(result);
        }

        [HttpGet("get-all-brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _mediator.Send(new GetAllBrandsQuery());
            return Ok(result);
        }

        [HttpGet("get-all-types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _mediator.Send(new GetAllTypesQuery());
            return Ok(result);
        }
        [HttpGet("get-product-by-name")]
        public async Task<IActionResult> GetProductByName([FromQuery]string Name)
        {
            var result = await _mediator.Send(new GetProductByNameQuery() { Name=Name});
            return Ok(result);
        }
        [HttpGet("get-product-by-brand-name")]
        public async Task<IActionResult> GetProductByBrandName([FromQuery] string Brandname)
        {
            var result = await _mediator.Send(new GetProductByBrandQuery() { Brandname = Brandname });
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
