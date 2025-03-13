using Microsoft.AspNetCore.Mvc;

namespace Identity.Main.Controllers
{
    public class SystemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
