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
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
              return _context.Locations != null ? 
                          View(await _context.Locations.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Locations'  is null.");
        }

        // GET: Locations/Details/5
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

        // GET: Locations/Create
        [HttpGet]
        [Route("Locations/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Name,PostalCode,Province,Address,City,WebsiteUrl,Capacity")] Location location)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(location);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = "Successfully added location " + location.Name;
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Addition Failed - State Invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Addition Failed - Error " +  ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Locations/Edit/5
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

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Name,PostalCode,Province,Address,City,WebsiteUrl,Capacity")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(location);
                        await _context.SaveChangesAsync();
                        TempData[TempDataHelper.Success] = "Successfully edited location" + location.Name;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        if (!LocationExists(location.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            TempData[TempDataHelper.Error] = "Error occured while editing: " + ex.Message;
                            throw;
                        }
                    }
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Error: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error occured while editing: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Locations/Delete/5
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

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
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
                TempData[TempDataHelper.Success] = "Successfully deleted location" + location.Name;
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error occured while deleting: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
          return (_context.Locations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
