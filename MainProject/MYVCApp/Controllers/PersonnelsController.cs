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
    /// Handles all interactions with Personnel in the database.
    /// </summary>
    public class PersonnelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public PersonnelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets base list view for Personnel.
        /// </summary>
        /// <returns>List view for Personnel.</returns>
        [HttpGet]
        [Route("Personnels")]
        public async Task<IActionResult> Index()
        {
              return _context.Personnel != null ? 
                          View(await _context.Personnel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Personnel'  is null.");
        }

        /// <summary>
        /// Gets details view for a given Personnel.
        /// </summary>
        /// <param name="id">Primary key of that personnel.</param>
        /// <returns>Details view for the personnel if they exist, 404 otherwise.</returns>
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

        /// <summary>
        /// Gets creation form for a new Personnel.
        /// </summary>
        /// <returns>Creation form for a new Personnel.</returns>
        // GET: Personnels/Create
        [HttpGet]
        [Route("Personnels/Create")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new Personnel in the database.
        /// </summary>
        /// <param name="personnel">The form data.</param>
        /// <returns>Redirect to list view if successful, back to form if not.</returns>
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

        /// <summary>
        /// Gets the edit view for a given personnel.
        /// </summary>
        /// <param name="id">The primary key for that personnel.</param>
        /// <returns>The edit view for that personnel if they exist, 404 otherwise.</returns>
        [HttpGet]
        [Route("Personnels/Edit/{id}")]
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

        /// <summary>
        /// Edits a given personnel in the database.
        /// </summary>
        /// <param name="id">The primary key for the personnel.</param>
        /// <param name="personnel">The form data.</param>
        /// <returns>Redirect to list view if successful, back to edit form if error, 404 if it doesn't exist.</returns>
        // POST: Personnels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Personnels/Edit/{id}")]
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

        /// <summary>
        /// Gets the deletion confirmation view for a given personnel.
        /// </summary>
        /// <param name="id">The primary key for that personnel.</param>
        /// <returns>The deletion confirmation view if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Personnels/Delete/{id}")]
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

        /// <summary>
        /// Deletes a given personnel in the database.
        /// </summary>
        /// <param name="id">The primary key for the personnel.</param>
        /// <returns>Redirect to list view if successful, problem if one occurs.</returns>
        // POST: Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Personnels/Delete/{id}")]
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
