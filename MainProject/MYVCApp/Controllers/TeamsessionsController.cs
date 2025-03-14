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
    public class TeamsessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teamsessions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teamsessions.Include(t => t.SessionIdFkNavigation).Include(t => t.TeamFormationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teamsessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teamsessions == null)
            {
                return NotFound();
            }

            var teamsession = await _context.Teamsessions
                .Include(t => t.SessionIdFkNavigation)
                .Include(t => t.TeamFormationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.TeamFormationIdFk == id);
            if (teamsession == null)
            {
                return NotFound();
            }

            return View(teamsession);
        }

        // GET: Teamsessions/Create
        public IActionResult Create()
        {
            ViewData["SessionIdFk"] = new SelectList(_context.Sessions, "Id", "Id");
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id");
            return View();
        }

        // POST: Teamsessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamFormationIdFk,SessionIdFk,Score")] Teamsession teamsession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamsession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SessionIdFk"] = new SelectList(_context.Sessions, "Id", "Id", teamsession.SessionIdFk);
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id", teamsession.TeamFormationIdFk);
            return View(teamsession);
        }

        // GET: Teamsessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teamsessions == null)
            {
                return NotFound();
            }

            var teamsession = await _context.Teamsessions.FindAsync(id);
            if (teamsession == null)
            {
                return NotFound();
            }
            ViewData["SessionIdFk"] = new SelectList(_context.Sessions, "Id", "Id", teamsession.SessionIdFk);
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id", teamsession.TeamFormationIdFk);
            return View(teamsession);
        }

        // POST: Teamsessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamFormationIdFk,SessionIdFk,Score")] Teamsession teamsession)
        {
            if (id != teamsession.TeamFormationIdFk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamsession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsessionExists(teamsession.TeamFormationIdFk))
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
            ViewData["SessionIdFk"] = new SelectList(_context.Sessions, "Id", "Id", teamsession.SessionIdFk);
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id", teamsession.TeamFormationIdFk);
            return View(teamsession);
        }

        // GET: Teamsessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teamsessions == null)
            {
                return NotFound();
            }

            var teamsession = await _context.Teamsessions
                .Include(t => t.SessionIdFkNavigation)
                .Include(t => t.TeamFormationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.TeamFormationIdFk == id);
            if (teamsession == null)
            {
                return NotFound();
            }

            return View(teamsession);
        }

        // POST: Teamsessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teamsessions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Teamsessions'  is null.");
            }
            var teamsession = await _context.Teamsessions.FindAsync(id);
            if (teamsession != null)
            {
                _context.Teamsessions.Remove(teamsession);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsessionExists(int id)
        {
          return (_context.Teamsessions?.Any(e => e.TeamFormationIdFk == id)).GetValueOrDefault();
        }
    }
}
