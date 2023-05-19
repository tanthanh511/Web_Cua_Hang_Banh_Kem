using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeShop.Models;
using PagedList.Core;
using CakeShop.Helpper;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace CakeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTblTinTucsController : Controller
    {
        private readonly CuaHangBanhKemContext _context;
        public INotyfService _notifyService { get; }
        public AdminTblTinTucsController(CuaHangBanhKemContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;

        }

        // GET: Admin/AdminTblTinTucs

        public IActionResult Index(int? page)
        {
            // tạo 10 dữ liệu giống nhau 
            //var tin = _context.TblTinTucs.Find(781);
            //for (int i = 781; i < 790; i++)
            //{
            //    TblTinTuc tblTinTuc = new TblTinTuc();
            //    tblTinTuc.Title = tin.Title;
            //    tblTinTuc.Published = tin.Published;
            //    tblTinTuc.Alias = tin.Alias;
            //    tblTinTuc.IsHot = tin.IsHot;
            //    tblTinTuc.IsNewfeed = tin.IsNewfeed;
            //    tblTinTuc.CreatedDate = tin.CreatedDate;
            //    tblTinTuc.Contents = tin.Contents;
            //    tblTinTuc.MetaDesc = tin.Title;
            //    tblTinTuc.MetaKey = tin.Title;
            //    _context.Add(tblTinTuc);
            //    _context.SaveChanges();
            //}

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsTinTuc = _context.TblTinTucs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);

            PagedList<TblTinTuc> models = new PagedList<TblTinTuc>(lsTinTuc, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

       

        // GET: Admin/AdminTblTinTucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblTinTucs == null)
            {
                return NotFound();
            }

            var tblTinTuc = await _context.TblTinTucs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tblTinTuc == null)
            {
                return NotFound();
            }

            return View(tblTinTuc);
        }

        // GET: Admin/AdminTblTinTucs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTblTinTucs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] TblTinTuc tblTinTuc, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                // xử lí thumb
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(tblTinTuc.Title) + extension;
                    tblTinTuc.Thumb = await Utilities.UploadFile(fThumb, @"news", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(tblTinTuc.Thumb)) tblTinTuc.Thumb = "default.jpg";
                tblTinTuc.Alias = Utilities.SEOUrl(tblTinTuc.Title);
                tblTinTuc.CreatedDate = DateTime.Now;
                _context.Add(tblTinTuc);
                await _context.SaveChangesAsync();
                _notifyService.Success("Thêm tin tức thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tblTinTuc);
        }

        // GET: Admin/AdminTblTinTucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblTinTucs == null)
            {
                return NotFound();
            }

            var tblTinTuc = await _context.TblTinTucs.FindAsync(id);
            if (tblTinTuc == null)
            {
                return NotFound();
            }
            return View(tblTinTuc);
        }

        // POST: Admin/AdminTblTinTucs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] TblTinTuc tblTinTuc, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != tblTinTuc.PostId)
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
                        string imageName = Utilities.SEOUrl(tblTinTuc.Title) + extension;
                        tblTinTuc.Thumb = await Utilities.UploadFile(fThumb, @"news", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(tblTinTuc.Thumb)) tblTinTuc.Thumb = "default.jpg";
                    tblTinTuc.Alias = Utilities.SEOUrl(tblTinTuc.Title);
                    _context.Update(tblTinTuc);
                    _notifyService.Success("Cập nhật tin tức thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTinTucExists(tblTinTuc.PostId))
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
            return View(tblTinTuc);
        }

        // GET: Admin/AdminTblTinTucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblTinTucs == null)
            {
                return NotFound();
            }

            var tblTinTuc = await _context.TblTinTucs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tblTinTuc == null)
            {
                return NotFound();
            }

            return View(tblTinTuc);
        }

        // POST: Admin/AdminTblTinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblTinTucs == null)
            {
                return Problem("Entity set 'CuaHangBanhKemContext.TblTinTucs'  is null.");
            }
            var tblTinTuc = await _context.TblTinTucs.FindAsync(id);
            if (tblTinTuc != null)
            {
                _context.TblTinTucs.Remove(tblTinTuc);
            }
            
            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa tin tức thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool TblTinTucExists(int id)
        {
          return (_context.TblTinTucs?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
