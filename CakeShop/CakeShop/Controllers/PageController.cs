using CakeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace CakeShop.Controllers
{
    public class PageController : Controller
    {
        private readonly CuaHangBanhKemContext _context;

        public PageController(CuaHangBanhKemContext context)
        {
            _context = context;
        }



        //GET: page/Alias
        [Route("/page/{Alias}", Name = "PageDetails")]
        public IActionResult Details(string Alias)
        {
            if(string.IsNullOrEmpty(Alias))  return RedirectToAction("Index", "Home");
            var page = _context.Pages.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
            if (page == null)
            {
                return RedirectToAction("Index", "Home");
            }           
            return View(page);
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
