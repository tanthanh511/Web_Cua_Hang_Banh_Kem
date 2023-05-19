using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeShop.Models;
using PagedList.Core;
using AspNetCoreHero.ToastNotification.Abstractions;
using CakeShop.Helpper;

namespace CakeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly CuaHangBanhKemContext _context;
        public INotyfService _notifyService { get; }
        public AdminCategoriesController(CuaHangBanhKemContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/AdminCategories
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 5;
            var lsCategories = _context.Categories
                .AsNoTracking()
                .OrderByDescending(x => x.CatId);

            PagedList<Category> models = new PagedList<Category>(lsCategories, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);


        }


        // GET: Admin/AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/AdminCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCategories/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,CatName,Description,ParentId,Levels,Ordering,Published,Thumb,Title,Alias,MetaDesc,MetaKey,Cover,SchemaMarkup")] Category category, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                // xử lí thumb
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(category.CatName) + extension;
                    category.Thumb = await Utilities.UploadFile(fThumb, @"category", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(category.Thumb)) category.Thumb = "default.jpg";
                category.Alias = Utilities.SEOUrl(category.CatName);

                _context.Add(category);
                await _context.SaveChangesAsync();
                _notifyService.Success("Thêm danh mục thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/AdminCategories/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatId,CatName,Description,ParentId,Levels,Ordering,Published,Thumb,Title,Alias,MetaDesc,MetaKey,Cover,SchemaMarkup")] Category category, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != category.CatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    // xử lí thumb
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string imageName = Utilities.SEOUrl(category.CatName) + extension;
                        category.Thumb = await Utilities.UploadFile(fThumb, @"category", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(category.Thumb)) category.Thumb = "default.jpg";
                    category.Alias = Utilities.SEOUrl(category.CatName);

                    _context.Update(category);
                    _notifyService.Success("Cập nhật danh mục thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CatId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'CuaHangBanhKemContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa danh mục thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.CatId == id)).GetValueOrDefault();
        }
    }
}
