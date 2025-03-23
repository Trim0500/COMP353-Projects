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
    public class FamilymemberlocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FamilymemberlocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Familymemberlocations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Familymemberlocations.Include(f => f.FamilyMemberIdFkNavigation).Include(f => f.LocationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Familymemberlocations/Details/5
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

        // GET: Familymemberlocations/Create
        [HttpGet]
        [Route("Familymemberlocations/Create")]
        public IActionResult Create()
        {
            ViewData["FamilyMemberIdFk"] = new SelectList(_context.Familymembers, "Id", "Id");
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

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

        // GET: Familymemberlocations/Delete/5
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

        // POST: Familymemberlocations/Delete/5
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
