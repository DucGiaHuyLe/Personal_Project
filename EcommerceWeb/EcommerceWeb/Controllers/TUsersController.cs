using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceWeb.Data;
using EcommerceWeb.Models;

namespace EcommerceWeb.Controllers
{
    public class TUsersController : Controller
    {
        private readonly QLBanValiContext _context;

        public TUsersController(QLBanValiContext context)
        {
            _context = context;
        }

        // GET: TUsers
        public async Task<IActionResult> Index()
        {
              return View(await _context.TUsers.ToListAsync());
        }

        // GET: TUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TUsers == null)
            {
                return NotFound();
            }

            var tUser = await _context.TUsers
                .FirstOrDefaultAsync(m => m.Username == id);
            if (tUser == null)
            {
                return NotFound();
            }

            return View(tUser);
        }

        // GET: TUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,LoaiUser")] TUser tUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tUser);
        }

        // GET: TUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TUsers == null)
            {
                return NotFound();
            }

            var tUser = await _context.TUsers.FindAsync(id);
            if (tUser == null)
            {
                return NotFound();
            }
            return View(tUser);
        }

        // POST: TUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,LoaiUser")] TUser tUser)
        {
            if (id != tUser.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TUserExists(tUser.Username))
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
            return View(tUser);
        }

        // GET: TUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TUsers == null)
            {
                return NotFound();
            }

            var tUser = await _context.TUsers
                .FirstOrDefaultAsync(m => m.Username == id);
            if (tUser == null)
            {
                return NotFound();
            }

            return View(tUser);
        }

        // POST: TUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TUsers == null)
            {
                return Problem("Entity set 'QLBanValiContext.TUsers'  is null.");
            }
            var tUser = await _context.TUsers.FindAsync(id);
            if (tUser != null)
            {
                _context.TUsers.Remove(tUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TUserExists(string id)
        {
          return _context.TUsers.Any(e => e.Username == id);
        }
    }
}
