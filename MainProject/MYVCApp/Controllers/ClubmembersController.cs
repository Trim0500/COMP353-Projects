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
    /// Handles database interactions for ClubMembers.
    /// </summary>
    public class ClubmembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public ClubmembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Index page for ClubMembers.
        /// </summary>
        /// <returns>List view for ClubMember records.</returns>
        [HttpGet]
        [Route("Clubmembers")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Clubmembers.Include(c => c.FamilyMemberIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets details view for a ClubMember.
        /// </summary>
        /// <param name="id">The primary key of the ClubMember.</param>
        /// <returns>Form to edit the given ClubMember if exists, 404 if not.</returns>
        [HttpGet]
        [Route("Clubmembers/{id}")]
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

        /// <summary>
        /// Gets create view for a ClubMember.
        /// </summary>
        /// <returns>Form to create a new ClubMember.</returns>
        [HttpGet]
        [Route("Clubmembers/Create")]
        public IActionResult Create()
        {
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates a new ClubMember instance in the database.
        /// </summary>
        /// <param name="clubmember">ClubMember object to be inserted.</param>
        /// <returns>Redirect to list view upon success, back to the form otherwise.</returns>
        // POST: Clubmembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Clubmembers/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cmn,FirstName,LastName,Dob,Email,Height,Weight,SocialSecNum,MedCardNum,PhoneNumber,City,Province,PostalCode,Address,ProgressReport,IsActive,FamilyMemberIdFk,PrimaryRelationship,SecondaryRelationship,Gender")] Clubmember clubmember)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(clubmember);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = "Successfully created ClubMember with ID " + clubmember.Cmn;
                    return RedirectToAction(nameof(Index));
                }
                ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", clubmember.FamilyMemberIdFk);
                return View(clubmember);
            }
            catch(Exception ex) 
            {
                TempData[TempDataHelper.Error] = "Error creating ClubMember: " + ExceptionFormatter.GetFullMessage(ex);
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Edit view for a given ClubMember.
        /// </summary>
        /// <param name="id">Primary key for the ClubMember.</param>
        /// <returns>Edit view for the ClubMember if they exist, 404 otherwise.</returns>
        [HttpGet]
        [Route("Clubmembers/Edit/{id}")]
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

        /// <summary>
        /// Edits a given ClubMember.
        /// </summary>
        /// <param name="id">Primary key of the ClubMember.</param>
        /// <param name="clubmember">ClubMember form data.</param>
        /// <returns>Redirect to list view if successful, 404 otherwise.</returns>
        // POST: Clubmembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Clubmembers/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cmn,FirstName,LastName,Dob,Email,Height,Weight,SocialSecNum,MedCardNum,PhoneNumber,City,Province,PostalCode,Address,ProgressReport,IsActive,FamilyMemberIdFk,PrimaryRelationship,SecondaryRelationship,Gender")] Clubmember clubmember)
        {
            try
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
                        TempData[TempDataHelper.Success] = "Successfully edited ClubMember with ID" + id;
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
                else
                {
                    TempData[TempDataHelper.Error] = "Error: State invalid.";
                }
                ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", clubmember.FamilyMemberIdFk);
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error updating ClubMember: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            return View(clubmember);
        }

        /// <summary>
        /// Delete view for a ClubMember.
        /// </summary>
        /// <param name="id">Primary key for the ClubMember.</param>
        /// <returns>The delete confirmation view for the ClubMember if they exist, 404 otherwise.</returns>
        [HttpGet]
        [Route("Clubmembers/Delete/{id}")]
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

        /// <summary>
        /// Deletes a ClubMember in the database.
        /// </summary>
        /// <param name="id">The primary key of the ClubMember.</param>
        /// <returns>Problem if one occured, redirects to List view otherwise.</returns>
        // POST: Clubmembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Clubmembers/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
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
                TempData[TempDataHelper.Success] = "Successfully deleted ClubMember with ID " + id;
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting ClubMember: " + ExceptionFormatter.GetFullMessage(ex);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ClubmemberExists(int id)
        {
          return (_context.Clubmembers?.Any(e => e.Cmn == id)).GetValueOrDefault();
        }
    }
}
