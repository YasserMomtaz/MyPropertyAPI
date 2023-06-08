using Microsoft.AspNetCore.Mvc;

namespace MyPropertyAPI.Controllers
{
    public class PendingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
