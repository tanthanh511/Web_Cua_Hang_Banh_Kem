using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
