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

        // GET: Locationphones
        public async Task<IActionResult> Index()
        {
              return _context.Locationphones != null ? 
                          View(await _context.Locationphones.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Locationphones'  is null.");
        }

        [Route("{location}/{phone}")]
        public async Task<IActionResult> Details(int location, string phone)
        {
            if (location < 0|| phone == null || _context.Locationphones == null)
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

        // GET: Locationphones/Create
        [HttpGet]
        [Route("Locationphones/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locationphones/Create
        [HttpPost]
        [Route("Locationphones/Create")]
        public async Task<IActionResult> Create(Locationphone locationphone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationphone);
                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = String.Format("Successfully created LocationPhone: {0} - {1}", locationphone.LocationIdFk, locationphone.PhoneNumber);
                return RedirectToAction(nameof(Index));
            }
            TempData[TempDataHelper.Error] = "Creation failed";
            return View("Index");
        }

        // GET: Locationphones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locationphones == null)
            {
                return NotFound();
            }

            var locationphone = await _context.Locationphones
                .FirstOrDefaultAsync(m => m.LocationIdFk == id);
            if (locationphone == null)
            {
                return NotFound();
            }

            return View(locationphone);
        }

        // POST: Locationphones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Locationphones == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Locationphones'  is null.");
            }
            var locationphone = await _context.Locationphones.FindAsync(id);
            if (locationphone != null)
            {
                _context.Locationphones.Remove(locationphone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationphoneExists(int id)
        {
          return (_context.Locationphones?.Any(e => e.LocationIdFk == id)).GetValueOrDefault();
        }
    }
}
