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
    public class SecondaryfamilymembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecondaryfamilymembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Secondaryfamilymembers.Include(s => s.PrimaryFamilyMemberIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        [Route("Secondaryfamilymember/{primary}_{firstname}_{lastname}")]
        public async Task<IActionResult> Details(int primary, string firstname, string lastname)
        {
            if (primary < 0 || _context.Secondaryfamilymembers == null)
            {
                return NotFound();
            }

            var secondaryfamilymember = await _context.Secondaryfamilymembers
                .Include(s => s.PrimaryFamilyMemberIdFkNavigation)
                .FirstOrDefaultAsync(m => m.PrimaryFamilyMemberIdFk == primary && m.FirstName == firstname && m.LastName == lastname);

            if (secondaryfamilymember == null)
            {
                return NotFound();
            }

            return View(secondaryfamilymember);
        }

        [HttpGet]
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
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(secondaryfamilymember);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = String.Format("Successfully created SecondaryFamilyMember {0}_{1}_{2}", secondaryfamilymember.PrimaryFamilyMemberIdFk, secondaryfamilymember.FirstName, secondaryfamilymember.LastName);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Error creating SecondaryFamilyMember: State invalid";
                }
                ViewData["PrimaryFamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", secondaryfamilymember.PrimaryFamilyMemberIdFk);
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating SecondaryFamilyMember: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }
            return View(secondaryfamilymember);
        }

        [HttpGet]
        [Route("Secondaryfamilymember/{primary}_{firstname}_{lastname}/Edit")]
        public async Task<IActionResult> Edit(int primary, string firstname, string lastname)
        {
            if (primary < 0 || _context.Secondaryfamilymembers == null)
            {
                return NotFound();
            }

            var secondaryfamilymember = await _context.Secondaryfamilymembers
                .Include(s => s.PrimaryFamilyMemberIdFkNavigation)
                .FirstOrDefaultAsync(m => m.PrimaryFamilyMemberIdFk == primary && m.FirstName == firstname && m.LastName == lastname);

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
        [Route("Secondaryfamilymember/{primary}_{firstname}_{lastname}/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PrimaryFamilyMemberIdFk,FirstName,LastName,PhoneNumber,RelationshipToPrimary")] Secondaryfamilymember secondaryfamilymember)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(secondaryfamilymember);
                        await _context.SaveChangesAsync();
                        TempData[TempDataHelper.Success] = String.Format("Successfully edited SecondaryFamilyMember {0}_{1}_{2}", secondaryfamilymember.PrimaryFamilyMemberIdFk, secondaryfamilymember.FirstName, secondaryfamilymember.LastName);
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
                else
                {
                    TempData[TempDataHelper.Error] = "Error updating SecondaryFamilyMember: State invalid.";
                }
                ViewData["PrimaryFamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", secondaryfamilymember.PrimaryFamilyMemberIdFk);
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error updating SecondaryFamilyMember: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            return View(secondaryfamilymember);
        }

        [HttpGet]
        [Route("Secondaryfamilymember/{primary}_{firstname}_{lastname}/Delete")]
        public async Task<IActionResult> Delete(int primary, string firstname, string lastname)
        {
            if (primary < 0 || _context.Secondaryfamilymembers == null)
            {
                return NotFound();
            }

            var secondaryfamilymember = await _context.Secondaryfamilymembers
                .Include(s => s.PrimaryFamilyMemberIdFkNavigation)
                .FirstOrDefaultAsync(m => m.PrimaryFamilyMemberIdFk == primary && m.FirstName == firstname && m.LastName == lastname);

            if (secondaryfamilymember == null)
            {
                return NotFound();
            }

            return View(secondaryfamilymember);
        }

        // POST: Secondaryfamilymembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Secondaryfamilymember/{primary}_{firstname}_{lastname}/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int primary, string firstname, string lastname)
        {
            try
            {
                if (_context.Secondaryfamilymembers == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Secondaryfamilymembers'  is null.");
                }
                var secondaryfamilymember = await _context.Secondaryfamilymembers
                    .Include(s => s.PrimaryFamilyMemberIdFkNavigation)
                    .FirstOrDefaultAsync(m => m.PrimaryFamilyMemberIdFk == primary && m.FirstName == firstname && m.LastName == lastname);
                if (secondaryfamilymember != null)
                {
                    _context.Secondaryfamilymembers.Remove(secondaryfamilymember);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = String.Format("Successfully deleted SecondaryFamilyMember {0}_{1}_{2}", secondaryfamilymember.PrimaryFamilyMemberIdFk, secondaryfamilymember.FirstName, secondaryfamilymember.LastName);
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting SecondaryFamilyMember: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SecondaryfamilymemberExists(int id)
        {
          return (_context.Secondaryfamilymembers?.Any(e => e.PrimaryFamilyMemberIdFk == id)).GetValueOrDefault();
        }
    }
}
