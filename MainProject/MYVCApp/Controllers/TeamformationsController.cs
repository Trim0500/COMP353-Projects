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
    /// Handles interactions with TeamFormations in the database.
    /// </summary>
    public class TeamformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public TeamformationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets base list view for TeamFormations.
        /// </summary>
        /// <returns>List view for TeamFormations.</returns>
        [HttpGet]
        [Route("Teamformations")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teamformations.Include(t => t.CaptainIdFkNavigation).Include(t => t.LocationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets the details view for a TeamFormation in the system.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Teamformations/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teamformations == null)
            {
                return NotFound();
            }

            var teamformation = await _context.Teamformations
                .Include(t => t.CaptainIdFkNavigation)
                .Include(t => t.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teamformation == null)
            {
                return NotFound();
            }

            return View(teamformation);
        }

        /// <summary>
        /// Gets the creation form for a new TeamFormation.
        /// </summary>
        /// <returns>Creation form for a new TeamFormation.</returns>
        [HttpGet]
        [Route("Teamformations/Create")]
        public IActionResult Create()
        {
            ViewData["CaptainIdFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn");
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates a new TeamFormation in the database.
        /// </summary>
        /// <param name="teamformation">The form data.</param>
        /// <returns>Redirect to index/list if successful, back to form otherwise.</returns>
        // POST: Teamformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Teamformations/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CaptainIdFk,LocationIdFk")] Teamformation teamformation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(teamformation);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = "Successfully created TeamFormation with ID " + teamformation.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Error creating TeamFormation: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating TeamFormation: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CaptainIdFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", teamformation.CaptainIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", teamformation.LocationIdFk);
            return View(teamformation);
        }

        /// <summary>
        /// Gets the edit view for a given TeamFormations.
        /// </summary>
        /// <param name="id">The primary key of that TeamFormation.</param>
        /// <returns>The edit form if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Teamformations/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teamformations == null)
            {
                return NotFound();
            }

            var teamformation = await _context.Teamformations.FindAsync(id);
            if (teamformation == null)
            {
                return NotFound();
            }
            ViewData["CaptainIdFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", teamformation.CaptainIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", teamformation.LocationIdFk);
            return View(teamformation);
        }

        /// <summary>
        /// Edits a given TeamFormation in the database.
        /// </summary>
        /// <param name="id">The primary key of that TeamFormation.</param>
        /// <param name="teamformation">The form data.</param>
        /// <returns>Redirect to index if successful, back to form otherwise. 404 if record does not exist.</returns>
        // POST: Teamformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Teamformations/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CaptainIdFk,LocationIdFk")] Teamformation teamformation)
        {
            if (id != teamformation.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(teamformation);
                        await _context.SaveChangesAsync();
                        TempData[TempDataHelper.Success] = "Successfully edited TeamFormation with ID " + teamformation.Id;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TeamformationExists(teamformation.Id))
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
                    TempData[TempDataHelper.Error] = "Error editing TeamFormation: State invalid";
                }
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error editing TeamFormation: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CaptainIdFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", teamformation.CaptainIdFk);
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", teamformation.LocationIdFk);
            return View(teamformation);
        }

        /// <summary>
        /// Gets the deletion view for a given TeamFormations.
        /// </summary>
        /// <param name="id">The primary key of that TeamFormation.</param>
        /// <returns>The deletion confirmation form if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Teamformations/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teamformations == null)
            {
                return NotFound();
            }

            var teamformation = await _context.Teamformations
                .Include(t => t.CaptainIdFkNavigation)
                .Include(t => t.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teamformation == null)
            {
                return NotFound();
            }

            return View(teamformation);
        }

        /// <summary>
        /// Deletes a given Teamformation in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirect to index if successful, Problem if one occurs.</returns>
        // POST: Teamformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Teamformations/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Teamformations == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Teamformations'  is null.");
                }
                var teamformation = await _context.Teamformations.FindAsync(id);
                if (teamformation != null)
                {
                    _context.Teamformations.Remove(teamformation);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = "Successfully edited TeamFormation with ID " + teamformation.Id;
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting TeamFormation: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TeamformationExists(int id)
        {
          return (_context.Teamformations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
