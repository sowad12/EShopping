using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;

namespace Identity.Main.Controllers;

public class HomeController : Controller
{
    private readonly IIdentityServerInteractionService _interaction;
    public HomeController(IIdentityServerInteractionService interaction)
    {
        _interaction = interaction;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult InvalidRequest()
    {
        return View();
    }

    //public async Task<IActionResult> Error(string errorId)
    //{
    //    var vm = new ErrorViewModel();
    //    var message = await _interaction.GetErrorContextAsync(errorId);
    //    if (message != null)
    //        vm.Message = message;
    //    return View("Error", vm);
    //}
}
