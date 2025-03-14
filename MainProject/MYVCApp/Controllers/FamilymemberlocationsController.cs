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
    public class FamilymemberlocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FamilymemberlocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Familymemberlocations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Familymemberlocations.Include(f => f.FamilyMemberIdFkNavigation).Include(f => f.LocationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Familymemberlocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Familymemberlocations == null)
            {
                return NotFound();
            }

            var familymemberlocation = await _context.Familymemberlocations
                .Include(f => f.FamilyMemberIdFkNavigation)
                .Include(f => f.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.LocationIdFk == id);
            if (familymemberlocation == null)
            {
                return NotFound();
            }

            return View(familymemberlocation);
        }

        // GET: Familymemberlocations/Create
        public IActionResult Create()
        {
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id");
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

        // POST: Familymemberlocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationIdFk,FamilyMemberIdFk,StartDate,EndDate")] Familymemberlocation familymemberlocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(familymemberlocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", familymemberlocation.FamilyMemberIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", familymemberlocation.LocationIdFk);
            return View(familymemberlocation);
        }

        // GET: Familymemberlocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Familymemberlocations == null)
            {
                return NotFound();
            }

            var familymemberlocation = await _context.Familymemberlocations.FindAsync(id);
            if (familymemberlocation == null)
            {
                return NotFound();
            }
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", familymemberlocation.FamilyMemberIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", familymemberlocation.LocationIdFk);
            return View(familymemberlocation);
        }

        // POST: Familymemberlocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationIdFk,FamilyMemberIdFk,StartDate,EndDate")] Familymemberlocation familymemberlocation)
        {
            if (id != familymemberlocation.LocationIdFk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familymemberlocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilymemberlocationExists(familymemberlocation.LocationIdFk))
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
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", familymemberlocation.FamilyMemberIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", familymemberlocation.LocationIdFk);
            return View(familymemberlocation);
        }

        // GET: Familymemberlocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Familymemberlocations == null)
            {
                return NotFound();
            }

            var familymemberlocation = await _context.Familymemberlocations
                .Include(f => f.FamilyMemberIdFkNavigation)
                .Include(f => f.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.LocationIdFk == id);
            if (familymemberlocation == null)
            {
                return NotFound();
            }

            return View(familymemberlocation);
        }

        // POST: Familymemberlocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Familymemberlocations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Familymemberlocations'  is null.");
            }
            var familymemberlocation = await _context.Familymemberlocations.FindAsync(id);
            if (familymemberlocation != null)
            {
                _context.Familymemberlocations.Remove(familymemberlocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilymemberlocationExists(int id)
        {
          return (_context.Familymemberlocations?.Any(e => e.LocationIdFk == id)).GetValueOrDefault();
        }
    }
}
