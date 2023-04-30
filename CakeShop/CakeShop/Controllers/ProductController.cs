using CakeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly CuaHangBanhKemContext _context;

        public ProductController(CuaHangBanhKemContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {

            return View();
        }
    }
}
