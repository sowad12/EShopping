using Identity.Library.Domains.Commands.Account;
using Identity.Library.Domains.Commands.Entities;
using Identity.Library.Manager.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientManager _clientService;

        public ClientsController(IClientManager clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("create-client")]
        public async Task<ActionResult> Create([FromBody] CreateClientCommand command)
        {
            var result = await _clientService.CreateClient(command);
            return Ok(result);
        }

        //[HttpPost("create-identity-user")]
        //public async Task<ActionResult> CreateIdentityUser([FromBody] CreateIdentityUserViewModel command)
        //{
        //    var result = await _clientService.CreateIdentityUser(command);
        //    return Ok(result);
        //}

        [HttpDelete("{clientId}")]
        public async Task<ActionResult> Delete([FromRoute] int clientId)
        {
            var result = await _clientService.DeleteClient(clientId);
            return Ok(result);
        }

        //[HttpGet("get-client")]
        //public async Task<ActionResult> GetClient([FromQuery] long ClubId)
        //{
        //    var result = await _clientService.GetClient(ClubId);
        //    return Ok(result);
        //}

        //[HttpPut("change-password")]
        //public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        //{
        //    var result = await _clientService.ChangePassword(command).AsSuccess();
        //    return Ok(result);
        //}
    }
}
