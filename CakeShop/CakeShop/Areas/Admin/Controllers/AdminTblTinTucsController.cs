using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeShop.Models;
using PagedList.Core;

namespace CakeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTblTinTucsController : Controller
    {
        private readonly CuaHangBanhKemContext _context;

        public AdminTblTinTucsController(CuaHangBanhKemContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminTblTinTucs

        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 5;
            var lsTinTuc = _context.TblTinTucs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);

            PagedList<TblTinTuc> models = new PagedList<TblTinTuc>(lsTinTuc, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);


        }

        //public async Task<IActionResult> Index()
        //{
        //      return _context.TblTinTucs != null ? 
        //                  View(await _context.TblTinTucs.ToListAsync()) :
        //                  Problem("Entity set 'CuaHangBanhKemContext.TblTinTucs'  is null.");
        //}

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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] TblTinTuc tblTinTuc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblTinTuc);
                await _context.SaveChangesAsync();
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] TblTinTuc tblTinTuc)
        {
            if (id != tblTinTuc.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblTinTuc);
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
            return RedirectToAction(nameof(Index));
        }

        private bool TblTinTucExists(int id)
        {
          return (_context.TblTinTucs?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
