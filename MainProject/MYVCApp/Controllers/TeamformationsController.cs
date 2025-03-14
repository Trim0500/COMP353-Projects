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
    public class TeamformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamformationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teamformations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teamformations.Include(t => t.CaptainIdFkNavigation).Include(t => t.LocationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teamformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teamformations == null)
            {
                return NotFound();
            }

            var teamformation = await _context.Teamformations
                .Include(t => t.CaptainIdFkNavigation)
                .Include(t => t.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamformation == null)
            {
                return NotFound();
            }

            return View(teamformation);
        }

        // GET: Teamformations/Create
        public IActionResult Create()
        {
            ViewData["CaptainIdFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn");
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

        // POST: Teamformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CaptainIdFk,LocationIdFk")] Teamformation teamformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaptainIdFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", teamformation.CaptainIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", teamformation.LocationIdFk);
            return View(teamformation);
        }

        // GET: Teamformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teamformations == null)
            {
                return NotFound();
            }

            var teamformation = await _context.Teamformations.FindAsync(id);
            if (teamformation == null)
            {
                return NotFound();
            }
            ViewData["CaptainIdFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", teamformation.CaptainIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", teamformation.LocationIdFk);
            return View(teamformation);
        }

        // POST: Teamformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CaptainIdFk,LocationIdFk")] Teamformation teamformation)
        {
            if (id != teamformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamformationExists(teamformation.Id))
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
            ViewData["CaptainIdFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", teamformation.CaptainIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", teamformation.LocationIdFk);
            return View(teamformation);
        }

        // GET: Teamformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teamformations == null)
            {
                return NotFound();
            }

            var teamformation = await _context.Teamformations
                .Include(t => t.CaptainIdFkNavigation)
                .Include(t => t.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamformation == null)
            {
                return NotFound();
            }

            return View(teamformation);
        }

        // POST: Teamformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teamformations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Teamformations'  is null.");
            }
            var teamformation = await _context.Teamformations.FindAsync(id);
            if (teamformation != null)
            {
                _context.Teamformations.Remove(teamformation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamformationExists(int id)
        {
          return (_context.Teamformations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
