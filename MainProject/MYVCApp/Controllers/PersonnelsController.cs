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
    public class PersonnelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonnelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personnels
        public async Task<IActionResult> Index()
        {
              return _context.Personnel != null ? 
                          View(await _context.Personnel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Personnel'  is null.");
        }

        // GET: Personnels/Details/5
        [HttpGet]
        [Route("Personnels/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personnel == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // GET: Personnels/Create
        [HttpGet]
        [Route("Personnels/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personnels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Personnels/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Dob,SocialSecNum,MedCardNum,PhoneNumber,City,Province,PostalCode,Email,Mandate")] Personnel personnel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(personnel);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = "Successfully created Personnel with ID " + personnel.Id;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating Personnel: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }
            return View(personnel);
        }

        // GET: Personnels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personnel == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel.FindAsync(id);
            if (personnel == null)
            {
                return NotFound();
            }
            return View(personnel);
        }

        // POST: Personnels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Dob,SocialSecNum,MedCardNum,PhoneNumber,City,Province,PostalCode,Email,Mandate")] Personnel personnel)
        {
            if (id != personnel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personnel);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = "Successfully edited Personnel with ID " + personnel.Id;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnelExists(personnel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(Exception ex)
                {
                    TempData[TempDataHelper.Error] = "Error editing Personnel: " + ExceptionFormatter.GetFullMessage(ex);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(personnel);
        }

        // GET: Personnels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personnel == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // POST: Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Personnel == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Personnel'  is null.");
                }
                var personnel = await _context.Personnel.FindAsync(id);
                if (personnel != null)
                {
                    _context.Personnel.Remove(personnel);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = "Successfully deleted Personnel with ID " + personnel.Id;
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PersonnelExists(int id)
        {
          return (_context.Personnel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
