using Microsoft.AspNetCore.Mvc;
using CakeShop.Models;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Details(int id)
        {
            var product = _context.Products.Include(x => x.CatId).FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(product);
        }
    }
}
