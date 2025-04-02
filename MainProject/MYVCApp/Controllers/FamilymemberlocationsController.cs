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
    /// Handles database interactions for FamilyMembers.
    /// </summary>
    public class FamilymemberlocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public FamilymemberlocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Base list view for FamilyMemberLocations.
        /// </summary>
        /// <returns>List view for FamilyMemberLocations.</returns>
        [HttpGet]
        [Route("Familymemberlocations")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Familymemberlocations.Include(f => f.FamilyMemberIdFkNavigation).Include(f => f.LocationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Retrieves a given FamilyMemberLocation
        /// </summary>
        /// <param name="familymember">The family member's ID.</param>
        /// <param name="location">The family member's location ID.</param>
        /// <param name="startdate">The family member's start date.</param>
        /// <returns>Details view for the given FamilyMemberLocation, or 404 if it doesn't exist.</returns>
        [HttpGet]
        [Route("Familymemberlocations/{familymember}/{location}/{startdate}")]
        public async Task<IActionResult> Details(int familymember, int location, DateTime startdate)
        {
            if (familymember < 0 || location < 0 || _context.Familymemberlocations == null)
            {
                return NotFound();
            }

            var familymemberlocation = await _context.Familymemberlocations
                .Include(f => f.FamilyMemberIdFkNavigation)
                .Include(f => f.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.LocationIdFk == location && m.FamilyMemberIdFk == familymember && m.StartDate == startdate);

            if (familymemberlocation == null)
            {
                return NotFound();
            }

            return View(familymemberlocation);
        }

        /// <summary>
        /// Create view for a new FamilyMemberLocation.
        /// </summary>
        /// <returns>FamilyMemberLocation creation form.</returns>
        [HttpGet]
        [Route("Familymemberlocations/Create")]
        public IActionResult Create()
        {
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id");
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates a new FamilyMemberLocation in the database.
        /// </summary>
        /// <param name="familymemberlocation">The new record to be created.</param>
        /// <returns>Redirect back to list view if successful, back to form otherwise.</returns>
        // POST: Familymemberlocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Familymemberlocations/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationIdFk,FamilyMemberIdFk,StartDate,EndDate")] Familymemberlocation familymemberlocation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(familymemberlocation);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = String.Format("Successfully created FamilyMemberLocation {0}/{1}/{2}", familymemberlocation.FamilyMemberIdFk, familymemberlocation.LocationIdFk, familymemberlocation.StartDate);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Error creating FamilyMemberLocation: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating FamilyMemberLocation " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", familymemberlocation.FamilyMemberIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", familymemberlocation.LocationIdFk);
            return View(familymemberlocation);
        }

        /// <summary>
        /// Edit form for a given FamilyMemberLocation.
        /// </summary>
        /// <param name="familymember">The ID of the FamilyMember.</param>
        /// <param name="location">The location ID of the FamilyMember.</param>
        /// <param name="startdate">The FamilyMember's start date.</param>
        /// <returns>Edit form for the given family member or 404 if it doesn't exist.</returns>
        [HttpGet]
        [Route("Familymemberlocations/{familymember}/{location}/{startdate}/Edit")]
        public async Task<IActionResult> Edit(int familymember, int location, DateTime startdate)
        {
            if (familymember < 0 || location < 0 || _context.Familymemberlocations == null)
            {
                return NotFound();
            }

            //var familymemberlocation = await _context.Familymemberlocations.FindAsync(id);
            var familymemberlocation = await _context.Familymemberlocations
                .Include(f => f.FamilyMemberIdFkNavigation)
                .Include(f => f.LocationIdFkNavigation)
                .FirstOrDefaultAsync(f => f.LocationIdFk == location && f.FamilyMemberIdFk == familymember && f.StartDate == startdate);

            if (familymemberlocation == null)
            {
                return NotFound();
            }
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", familymemberlocation.FamilyMemberIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", familymemberlocation.LocationIdFk);
            return View(familymemberlocation);
        }

        /// <summary>
        /// Edits a given FamilyMemberLocation in the database.
        /// </summary>
        /// <param name="familymemberlocation">The record to be updated.</param>
        /// <returns>Redirect to list view if successful, back to form otherwise if failed, 404 if record does not exist.</returns>
        // POST: Familymemberlocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Familymemberlocations/{familymember}/{location}/{startdate}/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("LocationIdFk,FamilyMemberIdFk,StartDate,EndDate")] Familymemberlocation familymemberlocation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(familymemberlocation);
                        await _context.SaveChangesAsync();
                        TempData[TempDataHelper.Success] = String.Format("Successfully edited FamilyMemberLocation {0}/{1}/{2}", familymemberlocation.FamilyMemberIdFk, familymemberlocation.LocationIdFk, familymemberlocation.StartDate);
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
                else
                {
                    TempData[TempDataHelper.Error] = "Error editing FamilyMemberLocation: State invalid";
                }
                ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id", familymemberlocation.FamilyMemberIdFk);
                ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", familymemberlocation.LocationIdFk);
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error editing FamilyMemberLocation: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            return View(familymemberlocation);
        }

        /// <summary>
        /// Deletion form for a given FamilyMemberLocation.
        /// </summary>
        /// <param name="familymember">The FamilyMember ID.</param>
        /// <param name="location">The FamilyMember location ID.</param>
        /// <param name="startdate">The start date for the FamilyMember.</param>
        /// <returns>Deletion confirmation form for FamilyMemberLocation.</returns>
        [HttpGet]
        [Route("Familymemberlocations/{familymember}/{location}/{startdate}/Delete")]
        public async Task<IActionResult> Delete(int familymember, int location, DateTime startdate)
        {
            if (familymember < 0 || location < 0 || _context.Familymemberlocations == null)
            {
                return NotFound();
            }

            var familymemberlocation = await _context.Familymemberlocations
                .Include(f => f.FamilyMemberIdFkNavigation)
                .Include(f => f.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.LocationIdFk == location && m.FamilyMemberIdFk == familymember && m.StartDate == startdate);

            if (familymemberlocation == null)
            {
                return NotFound();
            }

            return View(familymemberlocation);
        }

        /// <summary>
        /// Deletes a given FamilyMemberLocation in the database.
        /// </summary>
        /// <param name="familymember">The FamilyMember's ID.</param>
        /// <param name="location">The FamilyMember's location ID.</param>
        /// <param name="startdate">The FamilyMember's start date.</param>
        /// <returns>Redirects to list view if successful, problem if one occurs.</returns>
        [HttpPost, ActionName("Delete")]
        [Route("Familymemberlocations/{familymember}/{location}/{startdate}/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int familymember, int location, DateTime startdate)
        {
            try
            {
                if (_context.Familymemberlocations == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Familymemberlocations'  is null.");
                }

                var familymemberlocation = await _context.Familymemberlocations
                    .Include(f => f.FamilyMemberIdFkNavigation)
                    .Include(f => f.LocationIdFkNavigation)
                    .FirstOrDefaultAsync(m => m.LocationIdFk == location && m.FamilyMemberIdFk == familymember && m.StartDate == startdate);

                if (familymemberlocation != null)
                {
                    _context.Familymemberlocations.Remove(familymemberlocation);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = String.Format("Successfully deleted FamilyMemberLocation {0}/{1}/{2}", familymemberlocation.FamilyMemberIdFk, familymemberlocation.LocationIdFk, familymemberlocation.StartDate);
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting FamilyMemberLocation: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FamilymemberlocationExists(int id)
        {
          return (_context.Familymemberlocations?.Any(e => e.LocationIdFk == id)).GetValueOrDefault();
        }
    }
}
