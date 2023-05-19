using AspNetCoreHero.ToastNotification.Abstractions;
using CakeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace CakeShop.Controllers
{
    public class BlogController : Controller
    {       
        private readonly CuaHangBanhKemContext _context;

        public BlogController(CuaHangBanhKemContext context)
        {
            _context = context;
        }

        //GET: Blogs/Index
        //link route thay controller and action
        [Route("blogs.html", Name = ("Blog"))]
        public IActionResult Index(int? page)
        {
            // AsNoTracking: vô hiệu hóa việ theo dõi thay đổi để tối cải thiện hiệu suất và giảm sử dụng bộ nhớ
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsTinTuc = _context.TblTinTucs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);
            // OrderByDescending: sắp xếp giảm dần
            PagedList<TblTinTuc> models = new PagedList<TblTinTuc>(lsTinTuc, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);

        }


        //GET: Blogs/Details/5
        [Route("/tin-tuc/{Alias}-{id}.html", Name = "TinChiTiet")]
        public  IActionResult Details(int id)
        {
            //SingleOrDefault trả về một phần tử duy nhất hoặc null 
            var tintuc = _context.TblTinTucs.AsNoTracking().SingleOrDefault(x=> x.PostId==id);
            if (tintuc == null )
            {
                return RedirectToAction("Index");
            }

            //xuất ra 3 bài viết liên quan 
            var lsBaiVietLienQuan = _context.TblTinTucs
                .AsNoTracking()
                .Where(x => x.Published==true && x.PostId != id)
                .Take(3)
                .OrderByDescending (x => x.CreatedDate)
                .ToList();
            ViewBag.BaiVietLienQuan = lsBaiVietLienQuan;
            return View(tintuc);
        }
       
    }
}
