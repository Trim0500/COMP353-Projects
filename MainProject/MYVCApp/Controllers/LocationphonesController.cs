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
    public class LocationphonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationphonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Locationphones.Include(l => l.LocationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

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


        [HttpGet]
        [Route("Locationphones/Create")]
        public IActionResult Create()
        {
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

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
