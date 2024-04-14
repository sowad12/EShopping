using Catalog.Application.Commands;
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
        [HttpGet("get-movies")]
        public async Task<IActionResult> GetMovie()
        {
            //var res = await _dbContext.Movie.ToListAsync();
            return Ok(true);
        }
        [HttpPost("create-product")]   
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }
    }
}
