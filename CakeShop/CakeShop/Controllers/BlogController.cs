using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
