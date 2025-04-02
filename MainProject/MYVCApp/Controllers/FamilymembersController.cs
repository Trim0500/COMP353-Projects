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
    /// <summary>
    /// Handles all interactions with FamilyMembers in the database.
    /// </summary>
    public class FamilymembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public FamilymembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Base list view for FamilyMembers.
        /// </summary>
        /// <returns>List view for FamilyMembers.</returns>
        [HttpGet]
        [Route("Familymembers")]
        public async Task<IActionResult> Index()
        {
              return _context.Familymembers != null ? 
                          View(await _context.Familymembers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Familymembers'  is null.");
        }

        /// <summary>
        /// Details view for a given FamilyMember.
        /// </summary>
        /// <param name="id">The primary key of the FamilyMember record.</param>
        /// <returns>The details for that FamilyMember, or 404 if it doesn't exist.</returns>
        [HttpGet]
        [Route("Familymembers/{id}")]
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

        /// <summary>
        /// Create form for a FamilyMember.
        /// </summary>
        /// <returns>Form to create a new FamilyMember object in the database.</returns>
        [HttpGet]
        [Route("Familymembers/Create")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new FamilyMember record in the database.
        /// </summary>
        /// <param name="familymember">The form data.</param>
        /// <returns>Redirect to list view if successful, returns to form if not.</returns>
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

        /// <summary>
        /// Gets edit form for a given family member.
        /// </summary>
        /// <param name="id">The primary key of the FamilyMember.</param>
        /// <returns>Edit form for that family member, or 404 if it does not exist.</returns>
        [HttpGet]
        [Route("Familymembers/Edit/{id}")]
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

        /// <summary>
        /// Edits a given FamilyMember in the database.
        /// </summary>
        /// <param name="id">The primary key for the FamilyMember.</param>
        /// <param name="familymember">The form data.</param>
        /// <returns>Redirects to list if successful, returns to form otherwise.</returns>
        // POST: Familymembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Familymembers/Edit/{id}")]
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

        /// <summary>
        /// Gets deletion view for a given FamilyMember.
        /// </summary>
        /// <param name="id">The primary key of the FamilyMember.</param>
        /// <returns>The deletion view if the FamilyMember exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Familymembers/Delete/{id}")]
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

        /// <summary>
        /// Deletes a given FamilyMember from the database.
        /// </summary>
        /// <param name="id">The primary key of the FamilyMember.</param>
        /// <returns>Redirects to list view if successful, problem if it occurs.</returns>
        // POST: Familymembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Familymembers/Delete/{id}")]
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
