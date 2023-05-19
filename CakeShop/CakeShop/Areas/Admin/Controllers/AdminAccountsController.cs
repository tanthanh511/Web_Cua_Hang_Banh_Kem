 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeShop.Models;

namespace CakeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountsController : Controller
    {
        private readonly CuaHangBanhKemContext _context;
      
        public AdminAccountsController(CuaHangBanhKemContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminAccounts
        public async Task<IActionResult> Index()
        {
            ViewData["QuyenTruyCap"] = new SelectList(_context.Roles, "RoleId", "Description");

            List<SelectListItem> lsTrangThai = new List<SelectListItem>();
            lsTrangThai.Add(new SelectListItem() { Text = "Active", Value = "1" });
            lsTrangThai.Add(new SelectListItem() { Text = "Block", Value = "0" });
            ViewData["lsTrangThai"] = lsTrangThai;

            var CuaHangBanhKemContext = _context.Accounts.Include(a => a.RoleId);
              return _context.Accounts != null ? 
                          View(await _context.Accounts.ToListAsync()) :
                          Problem("Entity set 'CuaHangBanhKemContext.Accounts'  is null.");
        }

        // GET: Admin/AdminAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/AdminAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminAccounts/Create
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Phone,Email,Password,Salt,Active,FullName,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Admin/AdminAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Admin/AdminAccounts/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Phone,Email,Password,Salt,Active,FullName,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
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
            return View(account);
        }

        // GET: Admin/AdminAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'CuaHangBanhKemContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
