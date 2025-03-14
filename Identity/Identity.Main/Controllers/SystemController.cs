using Identity.Library.Manager.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemManager _systemService;
       
        public SystemController(ISystemManager systemService)
        {
            _systemService = systemService;        
        }

        [HttpGet("migrate")]
        public async Task<IActionResult> Migrate()
        {
            int result = await _systemService.Migrate();
            return Ok(result + " migrations are applied.");
        }


        [HttpGet("seed")]
        public async Task<IActionResult> Seed()
        {
            bool result = await _systemService.SeedDefault();
            return Ok(result ? "seeded." : "could not seed default data.");
        }


        //[HttpGet("ping")]
        //public async Task<IActionResult> Ping()
        //{
        //    await _bus.Publish(new PingFromIdentityServiceConsumerModel { Text = "Pinged From Identity" });
        //    return Ok("Pinged");
        //}

    }
}
