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
    /// Handles all interactions with PersonnelLocations in the database.
    /// </summary>
    public class PersonnellocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public PersonnellocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the base list view for PersonnelLocations.
        /// </summary>
        /// <returns>List view for PersonnelLocations.</returns>
        [HttpGet]
        [Route("Personnellocations")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Personnellocations.Include(p => p.LocationIdFkNavigation).Include(p => p.PersonnelIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets details for a given PersonnelLocation record in the system.
        /// </summary>
        /// <param name="personnel">The ID of the personnel.</param>
        /// <param name="location">Their location ID.</param>
        /// <param name="startDate">Their start date for that location.</param>
        /// <returns>Details view if it exists, 404 if not.</returns>
        [HttpGet]
        [Route("Personnellocations/{personnel}/{location}/{startdate}")]
        public async Task<IActionResult> Details(int personnel, int location, DateTime startDate)
        {
            if (startDate == null || _context.Personnellocations == null)
            {
                return NotFound();
            }

            var personnellocation = await _context.Personnellocations
                .Include(p => p.LocationIdFkNavigation)
                .Include(p => p.PersonnelIdFkNavigation)
                .FirstOrDefaultAsync(m => m.PersonnelIdFk == personnel && m.LocationIdFk == location && m.StartDate == startDate) ;
            if (personnellocation == null)
            {
                return NotFound();
            }

            return View(personnellocation);
        }

        /// <summary>
        /// Gets creation form to create a new PersonnelLocation.
        /// </summary>
        /// <returns>Creation form for a new Personnellocation.</returns>
        // GET: Personnellocations/Create
        [HttpGet]
        [Route("Personnellocations/Create")]
        public IActionResult Create()
        {
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id");
            ViewData["PersonnelIdFk"] = new SelectList(_context.Personnel, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates a new PersonnelLocation record in the database.
        /// </summary>
        /// <param name="personnellocation">The form data.</param>
        /// <returns>Redirect to list view if successful, back to form otherwise.</returns>
        // POST: Personnellocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Personnellocations/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonnelIdFk,LocationIdFk,StartDate,EndDate,Role")] Personnellocation personnellocation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(personnellocation);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = String.Format("Successfully created PersonnelLocation {0}/{1}/{2}", personnellocation.PersonnelIdFk, personnellocation.LocationIdFk, personnellocation.StartDate); 
                    return RedirectToAction(nameof(Index));
                }
                ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", personnellocation.LocationIdFk);
                ViewData["PersonnelIdFk"] = new SelectList(_context.Personnel, "Id", "Id", personnellocation.PersonnelIdFk);
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error while creating: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }
            return View(personnellocation);
        }

        /// <summary>
        /// Gets the edit form for a given PersonnelLocation.
        /// </summary>
        /// <param name="personnel">The personnel ID.</param>
        /// <param name="location">The location ID.</param>
        /// <param name="startdate">Their start date for that location.</param>
        /// <returns>The edit view if it exists, 404 otherwise.</returns>
        // GET: Personnellocations/Edit/5
        [HttpGet]
        [Route("Personnellocations/{personnel}/{location}/{startdate}/Edit")]
        public async Task<IActionResult> Edit(int personnel, int location, DateTime startdate)
        {
            if (_context.Personnellocations == null)
            {
                return NotFound();
            }

            //var personnellocation = await _context.Personnellocations.FindAsync(id);
            var personnellocation = await _context.Personnellocations
                .Include(p => p.PersonnelIdFkNavigation)
                .Include(l => l.LocationIdFkNavigation)
                .FirstOrDefaultAsync(p => p.PersonnelIdFk == personnel && p.LocationIdFk == location && p.StartDate == startdate);

            if (personnellocation == null)
            {
                return NotFound();
            }
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", personnellocation.LocationIdFk);
            ViewData["PersonnelIdFk"] = new SelectList(_context.Personnel, "Id", "Id", personnellocation.PersonnelIdFk);
            return View(personnellocation);
        }

        /// <summary>
        /// Edits a given PersonnelLocation in the database.
        /// </summary>
        /// <param name="personnellocation">The form data.</param>
        /// <returns>Redirect to list view if successful, back to form if error, 404 if not found.</returns>
        // POST: Personnellocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Personnellocations/{personnel}/{location}/{startdate}/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PersonnelIdFk,LocationIdFk,StartDate,EndDate,Role")] Personnellocation personnellocation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(personnellocation);
                        await _context.SaveChangesAsync();
                        TempData[TempDataHelper.Success] = String.Format("Successfully edited PersonnelLocation {0}/{1}/{2}", personnellocation.PersonnelIdFk, personnellocation.LocationIdFk, personnellocation.StartDate);
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
                else
                {
                    TempData[TempDataHelper.Error] = "Error while editing: State invalid";
                }
                ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", personnellocation.LocationIdFk);
                ViewData["PersonnelIdFk"] = new SelectList(_context.Personnel, "Id", "Id", personnellocation.PersonnelIdFk);
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error while editing: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            return View(personnellocation);
        }

        /// <summary>
        /// Gets deletion confirmation view for a given PersonnelLocation.
        /// </summary>
        /// <param name="personnel">The personnel ID.</param>
        /// <param name="location">The location ID.</param>
        /// <param name="startdate">Their start date for that location.</param>
        /// <returns>Deletion confirmation view if it exists, 404 if not.</returns>
        [HttpGet]
        [Route("Personnellocations/{personnel}/{location}/{startdate}/Delete")]
        public async Task<IActionResult> Delete(int personnel, int location, DateTime startdate)
        {
            if (startdate== null || _context.Personnellocations == null)
            {
                return NotFound();
            }

            var personnellocation = await _context.Personnellocations
                .Include(p => p.LocationIdFkNavigation)
                .Include(p => p.PersonnelIdFkNavigation)
                .FirstOrDefaultAsync(m => m.PersonnelIdFk == personnel && m.LocationIdFk == location && m.StartDate == startdate);
            if (personnellocation == null)
            {
                return NotFound();
            }

            return View(personnellocation);
        }

        /// <summary>
        /// Deletes a given PersonnelLocation record in the database.
        /// </summary>
        /// <param name="personnel">The personnel ID.</param>
        /// <param name="location">The location ID.</param>
        /// <param name="startdate">Their start date for that location.</param>
        /// <returns>Redirect to index if successful, problem if one occured.</returns>
        // POST: Personnellocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Personnellocations/{personnel}/{location}/{startdate}/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int personnel, int location, DateTime startdate)
        {
            try
            {
                if (_context.Personnellocations == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Personnellocations'  is null.");
                }
                var personnellocation = await _context.Personnellocations
                    .Include(p => p.LocationIdFkNavigation)
                    .Include(p => p.PersonnelIdFkNavigation)
                    .FirstOrDefaultAsync(m => m.PersonnelIdFk == personnel && m.LocationIdFk == location && m.StartDate == startdate);

                if (personnellocation != null)
                {
                    _context.Personnellocations.Remove(personnellocation);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = String.Format("Successfully deleted PersonnelLocation {0}/{1}/{2}", personnellocation.PersonnelIdFk, personnellocation.LocationIdFk, personnellocation.StartDate);
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PersonnellocationExists(int id)
        {
          return (_context.Personnellocations?.Any(e => e.PersonnelIdFk == id)).GetValueOrDefault();
        }
    }
}
