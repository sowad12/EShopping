
using Catalog.Repository.Manager.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SystemController : ControllerBase
    {
        private readonly ISystemManager _systemManager;
        public SystemController(ISystemManager systemManager)
        {
            _systemManager = systemManager;
        }

        [HttpPost("run-migration")]
        public async Task<IActionResult> Migrate()
        {
            var data = await _systemManager.RunMigration();
            return Ok(data);
        }


        [HttpPost("run-main-seeder")]
        public async Task<IActionResult> RunMainSeeder()
        {
            var data = await _systemManager.RunMainSeeder();
            return Ok(data);
        }

        [HttpPost("run-updater")]
        public async Task<IActionResult> RunUpdater()
        {
            var data = await _systemManager.RunUpdater();
            return Ok(data);
        }

        [HttpPost("StoredProcedure")]
        public async Task<IActionResult> StoredProcedure()
        {
            var result = await _systemManager.StoredProcedure();
            return Ok(result);
        }
    }

}
