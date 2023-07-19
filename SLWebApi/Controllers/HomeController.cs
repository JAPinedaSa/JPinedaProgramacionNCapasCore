using Microsoft.AspNetCore.Mvc;

namespace SLWebApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
