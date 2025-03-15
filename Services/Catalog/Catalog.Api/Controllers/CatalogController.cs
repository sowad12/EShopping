using Catalog.Application.Commands;
using Catalog.Application.Queries;
using EShopping.Core.Extensions;
using EShopping.Utilities.Pagination;
using EShopping.Utilities.Sort;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Authorize]
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

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand).AsCreated();
            return Ok(result);
        }

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetProducts([FromQuery] GetAllProductsQuery query)
        {
            var result = await _mediator.Send(query).AsSuccess();
            return Ok(result);
        }

        [HttpGet("get-product")]
        public async Task<IActionResult> GetProduct([FromQuery] GetProductByIdQuery getProductByIdQuery)
        {
            var result = await _mediator.Send(getProductByIdQuery).AsSuccess();
            return Ok(result);
        }

        [HttpGet("get-all-brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _mediator.Send(new GetAllBrandsQuery()).AsSuccess();
            return Ok(result);
        }

        [HttpGet("get-all-types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _mediator.Send(new GetAllTypesQuery()).AsSuccess();
            return Ok(result);
        }
        [HttpGet("get-product-by-name")]
        public async Task<IActionResult> GetProductByName([FromQuery] string Name)
        {
            var result = await _mediator.Send(new GetProductByNameQuery() { Name = Name }).AsSuccess();
            return Ok(result);
        }
        [HttpGet("get-product-by-brand-name")]
        public async Task<IActionResult> GetProductByBrandName([FromQuery] string Brandname)
        {
            var result = await _mediator.Send(new GetProductByBrandQuery() { Brandname = Brandname }).AsSuccess();
            return Ok(result);
        }

    }
}
