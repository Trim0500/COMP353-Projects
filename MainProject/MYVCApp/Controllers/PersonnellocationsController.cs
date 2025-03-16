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
    public class PersonnellocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonnellocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personnellocations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Personnellocations.Include(p => p.LocationIdFkNavigation).Include(p => p.PersonnelIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Personnellocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personnellocations == null)
            {
                return NotFound();
            }

            var personnellocation = await _context.Personnellocations
                .Include(p => p.LocationIdFkNavigation)
                .Include(p => p.PersonnelIdFkNavigation)
                .FirstOrDefaultAsync(m => m.PersonnelIdFk == id);
            if (personnellocation == null)
            {
                return NotFound();
            }

            return View(personnellocation);
        }

        // GET: Personnellocations/Create
        [HttpGet]
        [Route("Personnellocations/Create")]
        public IActionResult Create()
        {
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id");
            ViewData["PersonnelIdFk"] = new SelectList(_context.Personnel, "Id", "Id");
            return View();
        }

        // POST: Personnellocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonnelIdFk,LocationIdFk,StartDate,EndDate,Role")] Personnellocation personnellocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personnellocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", personnellocation.LocationIdFk);
            ViewData["PersonnelIdFk"] = new SelectList(_context.Personnel, "Id", "Id", personnellocation.PersonnelIdFk);
            return View(personnellocation);
        }

        // GET: Personnellocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personnellocations == null)
            {
                return NotFound();
            }

            var personnellocation = await _context.Personnellocations.FindAsync(id);
            if (personnellocation == null)
            {
                return NotFound();
            }
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", personnellocation.LocationIdFk);
            ViewData["PersonnelIdFk"] = new SelectList(_context.Personnel, "Id", "Id", personnellocation.PersonnelIdFk);
            return View(personnellocation);
        }

        // POST: Personnellocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonnelIdFk,LocationIdFk,StartDate,EndDate,Role")] Personnellocation personnellocation)
        {
            if (id != personnellocation.PersonnelIdFk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personnellocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnellocationExists(personnellocation.PersonnelIdFk))
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
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", personnellocation.LocationIdFk);
            ViewData["PersonnelIdFk"] = new SelectList(_context.Personnel, "Id", "Id", personnellocation.PersonnelIdFk);
            return View(personnellocation);
        }

        // GET: Personnellocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personnellocations == null)
            {
                return NotFound();
            }

            var personnellocation = await _context.Personnellocations
                .Include(p => p.LocationIdFkNavigation)
                .Include(p => p.PersonnelIdFkNavigation)
                .FirstOrDefaultAsync(m => m.PersonnelIdFk == id);
            if (personnellocation == null)
            {
                return NotFound();
            }

            return View(personnellocation);
        }

        // POST: Personnellocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personnellocations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Personnellocations'  is null.");
            }
            var personnellocation = await _context.Personnellocations.FindAsync(id);
            if (personnellocation != null)
            {
                _context.Personnellocations.Remove(personnellocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonnellocationExists(int id)
        {
          return (_context.Personnellocations?.Any(e => e.PersonnelIdFk == id)).GetValueOrDefault();
        }
    }
}
