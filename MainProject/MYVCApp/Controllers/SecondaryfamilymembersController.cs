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
    public class SecondaryfamilymembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecondaryfamilymembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Secondaryfamilymembers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Secondaryfamilymembers.Include(s => s.PrimaryFamilyMemberIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Secondaryfamilymembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Secondaryfamilymembers == null)
            {
                return NotFound();
            }

            var secondaryfamilymember = await _context.Secondaryfamilymembers
                .Include(s => s.PrimaryFamilyMemberIdFkNavigation)
                .FirstOrDefaultAsync(m => m.PrimaryFamilyMemberIdFk == id);
            if (secondaryfamilymember == null)
            {
                return NotFound();
            }

            return View(secondaryfamilymember);
        }

        // GET: Secondaryfamilymembers/Create
        public IActionResult Create()
        {
            ViewData["PrimaryFamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id");
            return View();
        }

        // POST: Secondaryfamilymembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrimaryFamilyMemberIdFk,FirstName,LastName,PhoneNumber,RelationshipToPrimary")] Secondaryfamilymember secondaryfamilymember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(secondaryfamilymember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrimaryFamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", secondaryfamilymember.PrimaryFamilyMemberIdFk);
            return View(secondaryfamilymember);
        }

        // GET: Secondaryfamilymembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Secondaryfamilymembers == null)
            {
                return NotFound();
            }

            var secondaryfamilymember = await _context.Secondaryfamilymembers.FindAsync(id);
            if (secondaryfamilymember == null)
            {
                return NotFound();
            }
            ViewData["PrimaryFamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", secondaryfamilymember.PrimaryFamilyMemberIdFk);
            return View(secondaryfamilymember);
        }

        // POST: Secondaryfamilymembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrimaryFamilyMemberIdFk,FirstName,LastName,PhoneNumber,RelationshipToPrimary")] Secondaryfamilymember secondaryfamilymember)
        {
            if (id != secondaryfamilymember.PrimaryFamilyMemberIdFk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secondaryfamilymember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecondaryfamilymemberExists(secondaryfamilymember.PrimaryFamilyMemberIdFk))
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
            ViewData["PrimaryFamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", secondaryfamilymember.PrimaryFamilyMemberIdFk);
            return View(secondaryfamilymember);
        }

        // GET: Secondaryfamilymembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Secondaryfamilymembers == null)
            {
                return NotFound();
            }

            var secondaryfamilymember = await _context.Secondaryfamilymembers
                .Include(s => s.PrimaryFamilyMemberIdFkNavigation)
                .FirstOrDefaultAsync(m => m.PrimaryFamilyMemberIdFk == id);
            if (secondaryfamilymember == null)
            {
                return NotFound();
            }

            return View(secondaryfamilymember);
        }

        // POST: Secondaryfamilymembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Secondaryfamilymembers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Secondaryfamilymembers'  is null.");
            }
            var secondaryfamilymember = await _context.Secondaryfamilymembers.FindAsync(id);
            if (secondaryfamilymember != null)
            {
                _context.Secondaryfamilymembers.Remove(secondaryfamilymember);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecondaryfamilymemberExists(int id)
        {
          return (_context.Secondaryfamilymembers?.Any(e => e.PrimaryFamilyMemberIdFk == id)).GetValueOrDefault();
        }
    }
}
