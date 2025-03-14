using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYVCApp.Contexts;
using MYVCApp.Models;

namespace MYVCApp.Controllers
{
    public class LocationphonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationphonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Locationphones
        public async Task<IActionResult> Index()
        {
              return _context.Locationphones != null ? 
                          View(await _context.Locationphones.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Locationphones'  is null.");
        }

        // GET: Locationphones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locationphones == null)
            {
                return NotFound();
            }

            var locationphone = await _context.Locationphones
                .FirstOrDefaultAsync(m => m.LocationIdFk == id);
            if (locationphone == null)
            {
                return NotFound();
            }

            return View(locationphone);
        }

        // GET: Locationphones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locationphones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationIdFk,PhoneNumber")] Locationphone locationphone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationphone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationphone);
        }

        // GET: Locationphones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Locationphones == null)
            {
                return NotFound();
            }

            var locationphone = await _context.Locationphones.FindAsync(id);
            if (locationphone == null)
            {
                return NotFound();
            }
            return View(locationphone);
        }

        // POST: Locationphones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationIdFk,PhoneNumber")] Locationphone locationphone)
        {
            if (id != locationphone.LocationIdFk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationphone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationphoneExists(locationphone.LocationIdFk))
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
            return View(locationphone);
        }

        // GET: Locationphones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locationphones == null)
            {
                return NotFound();
            }

            var locationphone = await _context.Locationphones
                .FirstOrDefaultAsync(m => m.LocationIdFk == id);
            if (locationphone == null)
            {
                return NotFound();
            }

            return View(locationphone);
        }

        // POST: Locationphones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Locationphones == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Locationphones'  is null.");
            }
            var locationphone = await _context.Locationphones.FindAsync(id);
            if (locationphone != null)
            {
                _context.Locationphones.Remove(locationphone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationphoneExists(int id)
        {
          return (_context.Locationphones?.Any(e => e.LocationIdFk == id)).GetValueOrDefault();
        }
    }
}
