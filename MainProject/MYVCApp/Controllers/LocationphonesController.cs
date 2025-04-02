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
    /// Controller handles LocationPhone objects in the database.
    /// </summary>
    public class LocationphonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected context.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public LocationphonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the list view for LocationPhones.
        /// </summary>
        /// <returns>The list view for LocationPhones.</returns>
        [HttpGet]
        [Route("Locationphones")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Locationphones.Include(l => l.LocationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets the details for a given LocationPhone record.
        /// </summary>
        /// <param name="location">The ID of the location.</param>
        /// <param name="phone">The phone number.</param>
        /// <returns>The details view for that record if it exists, 404 if not.</returns>
        [HttpGet]
        [Route("Locationphones/{location}/{phone}")]
        public async Task<IActionResult> Details(int location, string phone)
        {
            if (location < 0 || phone == null || _context.Locationphones == null)
            {
                return NotFound();
            }

            var locationphone = await _context.Locationphones
                .Include(l => l.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.LocationIdFk == location);

            if (locationphone == null)
            {
                return NotFound();
            }

            return View(locationphone);
        }

        /// <summary>
        /// Gets the creation form for a new LocationPhone record.
        /// </summary>
        /// <returns>Form to create a new LocationPhone record.</returns>
        [HttpGet]
        [Route("Locationphones/Create")]
        public IActionResult Create()
        {
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates a new LocationPhone in the database.
        /// </summary>
        /// <param name="locationphone">The form data.</param>
        /// <returns>Redirects to List view.</returns>
        [HttpPost]
        [Route("Locationphones/Create")]
        public async Task<IActionResult> Create([Bind("LocationIdFk,PhoneNumber")] Locationphone locationphone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(locationphone);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = String.Format("Successfully created LocationPhone: {0} - {1}", locationphone.LocationIdFk, locationphone.PhoneNumber);
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Creation failed, state invalid";
                }
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Creation failed: " + ExceptionFormatter.GetFullMessage(ex);
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Gets deletion confirmation for a given LocationPhone record in the database.
        /// </summary>
        /// <param name="location">The location ID of the record.</param>
        /// <param name="phone">The phone number for that location.</param>
        /// <returns>Deletion confirmation view for that LocationPhone, 404 if not found.</returns>
        [HttpGet]
        [Route("Personnellocations/{location}/{phone}/Delete")]
        public async Task<IActionResult> Delete(int location, string phone)
        {
            if (location < 0 || phone == null || _context.Locationphones == null)
            {
                return NotFound();
            }

            var locationphone = await _context.Locationphones
                .FirstOrDefaultAsync(m => m.LocationIdFk == location && m.PhoneNumber == phone);
            if (locationphone == null)
            {
                return NotFound();
            }

            return View(locationphone);
        }

        /// <summary>
        /// Deletes a give LocationPhone record in the database.
        /// </summary>
        /// <param name="location">The ID of the location.</param>
        /// <param name="phone">The phone number for the location.</param>
        /// <returns>Redirection to List view or Problem if one occurs.</returns>
        [HttpPost, ActionName("Delete")]
        [Route("Personnellocations/{location}/{phone}/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int location, string phone)
        {
            if (_context.Locationphones == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Locationphones'  is null.");
            }
            var locationphone = await _context.Locationphones
            .FirstOrDefaultAsync(m => m.LocationIdFk == location && m.PhoneNumber == phone);
            try
            {
                if (locationphone != null)
                {
                    _context.Locationphones.Remove(locationphone);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = String.Format("Successfully Deleted LocationPhone: {0} - {1}", locationphone.LocationIdFk, locationphone.PhoneNumber);
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Deletion Failed: Nonexistant Item";
                }
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Deletion Failed: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LocationphoneExists(int id)
        {
          return (_context.Locationphones?.Any(e => e.LocationIdFk == id)).GetValueOrDefault();
        }
    }
}
