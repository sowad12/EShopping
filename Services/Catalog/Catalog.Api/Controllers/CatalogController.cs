using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CatalogController : ControllerBase
    {
        [HttpGet("get-movies")]
        public async Task<IActionResult> GetMovie()
        {
            //var res = await _dbContext.Movie.ToListAsync();
            return Ok(true);
        }
    }
}
