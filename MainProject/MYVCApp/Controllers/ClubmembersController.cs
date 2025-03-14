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
    public class ClubmembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubmembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clubmembers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Clubmembers.Include(c => c.FamilyMemberIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Clubmembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clubmembers == null)
            {
                return NotFound();
            }

            var clubmember = await _context.Clubmembers
                .Include(c => c.FamilyMemberIdFkNavigation)
                .FirstOrDefaultAsync(m => m.Cmn == id);
            if (clubmember == null)
            {
                return NotFound();
            }

            return View(clubmember);
        }

        // GET: Clubmembers/Create
        public IActionResult Create()
        {
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id");
            return View();
        }

        // POST: Clubmembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cmn,FirstName,LastName,Dob,Email,Height,Weight,SocialSecNum,MedCardNum,PhoneNumber,City,Province,PostalCode,Address,ProgressReport,IsActive,FamilyMemberIdFk,PrimaryRelationship,SecondaryRelationship")] Clubmember clubmember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clubmember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", clubmember.FamilyMemberIdFk);
            return View(clubmember);
        }

        // GET: Clubmembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clubmembers == null)
            {
                return NotFound();
            }

            var clubmember = await _context.Clubmembers.FindAsync(id);
            if (clubmember == null)
            {
                return NotFound();
            }
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", clubmember.FamilyMemberIdFk);
            return View(clubmember);
        }

        // POST: Clubmembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cmn,FirstName,LastName,Dob,Email,Height,Weight,SocialSecNum,MedCardNum,PhoneNumber,City,Province,PostalCode,Address,ProgressReport,IsActive,FamilyMemberIdFk,PrimaryRelationship,SecondaryRelationship")] Clubmember clubmember)
        {
            if (id != clubmember.Cmn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clubmember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubmemberExists(clubmember.Cmn))
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
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", clubmember.FamilyMemberIdFk);
            return View(clubmember);
        }

        // GET: Clubmembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clubmembers == null)
            {
                return NotFound();
            }

            var clubmember = await _context.Clubmembers
                .Include(c => c.FamilyMemberIdFkNavigation)
                .FirstOrDefaultAsync(m => m.Cmn == id);
            if (clubmember == null)
            {
                return NotFound();
            }

            return View(clubmember);
        }

        // POST: Clubmembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clubmembers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Clubmembers'  is null.");
            }
            var clubmember = await _context.Clubmembers.FindAsync(id);
            if (clubmember != null)
            {
                _context.Clubmembers.Remove(clubmember);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubmemberExists(int id)
        {
          return (_context.Clubmembers?.Any(e => e.Cmn == id)).GetValueOrDefault();
        }
    }
}
