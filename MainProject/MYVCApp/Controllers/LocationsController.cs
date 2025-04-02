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
    /// Handles Location records in the database.
    /// </summary>
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets base list view for Locations.
        /// </summary>
        /// <returns>List view for Locations.</returns>
        [HttpGet]
        [Route("Locations")]
        public async Task<IActionResult> Index()
        {
            return _context.Locations != null ?
                        View(await _context.Locations.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Locations'  is null.");
        }

        /// <summary>
        /// Gets details for a given Location.
        /// </summary>
        /// <param name="id">Primary key of the given location.</param>
        /// <returns>The details for that location if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Locations/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        /// <summary>
        /// Gets the form to create a new Location record in the database.
        /// </summary>
        /// <returns>The form to create a new Location record.</returns>
        [HttpGet]
        [Route("Locations/Create")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new Location in the database.
        /// </summary>
        /// <param name="location">The form data.</param>
        /// <returns>Redirects to list view if successful, back to the form otherwise.</returns>
        // POST: Locations1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Locations/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Name,PostalCode,Province,Address,City,WebsiteUrl,Capacity")] Location location)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(location);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = "Successfully created Location with ID " + location.Id;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating Location: " + ex.Message + ex.InnerException != null ? ex.InnerException.Message : "";
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        /// <summary>
        /// Gets the edit view for a given location.
        /// </summary>
        /// <param name="id">The primary key of the location.</param>
        /// <returns>The edit form for that location if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Locations/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        /// <summary>
        /// Edits a given location in the database.
        /// </summary>
        /// <param name="id">The primary key for that location.</param>
        /// <param name="location">The form data.</param>
        /// <returns>Redirect to list view if successful, back to form if an error occurs, or 404 if not found.</returns>
        // POST: Locations1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Locations/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Name,PostalCode,Province,Address,City,WebsiteUrl,Capacity")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = "Successfully edited location with ID " + location.Id;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    TempData[TempDataHelper.Error] = "Error editing Location: " + ex.Message + ex.InnerException != null ? ex.InnerException.Message : "";
                }

                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        /// <summary>
        /// Gets the deletion confirmation view for a given Location.
        /// </summary>
        /// <param name="id">The primary key of that location.</param>
        /// <returns>Deletion confirmation view if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Locations/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        /// <summary>
        /// Deletes a given Location in the database.
        /// </summary>
        /// <param name="id">The primary key for that location.</param>
        /// <returns>Redirect to index if successful, Problem if one occurs.</returns>
        // POST: Locations1/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Locations/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Locations == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Locations'  is null.");
                }
                var location = await _context.Locations.FindAsync(id);
                if (location != null)
                {
                    _context.Locations.Remove(location);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = "Successfully deleted location with ID " + id;
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting: " + ex.Message + ex.InnerException != null ? ex.InnerException.Message : "";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return (_context.Locations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
