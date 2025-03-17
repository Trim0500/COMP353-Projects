using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYVCApp.Contexts;
using MYVCApp.Helpers;
using MYVCApp.Models;

namespace MYVCApp.Controllers
{
    public class FamilymembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FamilymembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Familymembers
        public async Task<IActionResult> Index()
        {
              return _context.Familymembers != null ? 
                          View(await _context.Familymembers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Familymembers'  is null.");
        }

        // GET: Familymembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Familymembers == null)
            {
                return NotFound();
            }

            var familymember = await _context.Familymembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (familymember == null)
            {
                return NotFound();
            }

            return View(familymember);
        }

        // GET: Familymembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Familymembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Dob,Email,SocialSecNum,MedCardNum,City,Province,PhoneNumber,PostalCode")] Familymember familymember)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(familymember);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = "Successfully created FamilyMember with ID " + familymember.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Error creating FamilyMember: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating FamilyMember: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            return View(familymember);
        }

        // GET: Familymembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Familymembers == null)
            {
                return NotFound();
            }

            var familymember = await _context.Familymembers.FindAsync(id);
            if (familymember == null)
            {
                return NotFound();
            }
            return View(familymember);
        }

        // POST: Familymembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Dob,Email,SocialSecNum,MedCardNum,City,Province,PhoneNumber,PostalCode")] Familymember familymember)
        {
            if (id != familymember.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(familymember);
                        await _context.SaveChangesAsync();
                        TempData[TempDataHelper.Success] = "Successfully edited FamilyMember with ID " + familymember.Id;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FamilymemberExists(familymember.Id))
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
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error editing FamilyMember: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            return View(familymember);
        }

        // GET: Familymembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Familymembers == null)
            {
                return NotFound();
            }

            var familymember = await _context.Familymembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (familymember == null)
            {
                return NotFound();
            }

            return View(familymember);
        }

        // POST: Familymembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            try
            {
                if (_context.Familymembers == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Familymembers'  is null.");
                }
                var familymember = await _context.Familymembers.FindAsync(id);
                if (familymember != null)
                {
                    _context.Familymembers.Remove(familymember);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = "Successfully deleted FamilyMember with ID " + familymember.Id;
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FamilymemberExists(int id)
        {
          return (_context.Familymembers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
