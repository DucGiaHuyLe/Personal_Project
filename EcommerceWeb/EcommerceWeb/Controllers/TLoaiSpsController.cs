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
    public class TLoaiSpsController : Controller
    {
        private readonly QLBanValiContext _context;

        public TLoaiSpsController(QLBanValiContext context)
        {
            _context = context;
        }

        // GET: TLoaiSps
        public async Task<IActionResult> Index()
        {
              return View(await _context.TLoaiSps.ToListAsync());
        }

        // GET: TLoaiSps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TLoaiSps == null)
            {
                return NotFound();
            }

            var tLoaiSp = await _context.TLoaiSps
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (tLoaiSp == null)
            {
                return NotFound();
            }

            return View(tLoaiSp);
        }

        // GET: TLoaiSps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TLoaiSps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,Loai")] TLoaiSp tLoaiSp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tLoaiSp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tLoaiSp);
        }

        // GET: TLoaiSps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TLoaiSps == null)
            {
                return NotFound();
            }

            var tLoaiSp = await _context.TLoaiSps.FindAsync(id);
            if (tLoaiSp == null)
            {
                return NotFound();
            }
            return View(tLoaiSp);
        }

        // POST: TLoaiSps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoai,Loai")] TLoaiSp tLoaiSp)
        {
            if (id != tLoaiSp.MaLoai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tLoaiSp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLoaiSpExists(tLoaiSp.MaLoai))
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
            return View(tLoaiSp);
        }

        // GET: TLoaiSps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TLoaiSps == null)
            {
                return NotFound();
            }

            var tLoaiSp = await _context.TLoaiSps
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (tLoaiSp == null)
            {
                return NotFound();
            }

            return View(tLoaiSp);
        }

        // POST: TLoaiSps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TLoaiSps == null)
            {
                return Problem("Entity set 'QLBanValiContext.TLoaiSps'  is null.");
            }
            var tLoaiSp = await _context.TLoaiSps.FindAsync(id);
            if (tLoaiSp != null)
            {
                _context.TLoaiSps.Remove(tLoaiSp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TLoaiSpExists(int id)
        {
          return _context.TLoaiSps.Any(e => e.MaLoai == id);
        }
    }
}
