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
    /// Handles all interactions with Sessions in the database.
    /// </summary>
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public SessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the base list view for Sessions.
        /// </summary>
        /// <returns>List view for Sessions.</returns>
        [HttpGet]
        [Route("Sessions")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sessions.Include(s => s.LocationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets the details view for a given Session.
        /// </summary>
        /// <param name="id">The primary key of that session.</param>
        /// <returns>The details view for the session if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Sessions/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        /// <summary>
        /// Gets the creation view for a new Session.
        /// </summary>
        /// <returns>Creation view for a new Session.</returns>
        [HttpGet]
        [Route("Sessions/Create")]
        public IActionResult Create()
        {
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates a new Session in the database.
        /// </summary>
        /// <param name="session">The form data.</param>
        /// <returns>Redirect to List if successful, back to form otherwise.</returns>
        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Sessions/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventType,EventDateTime,LocationIdFk")] Session session)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(session);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = "Successfully created Session with ID " + session.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Error creating Session: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating Session: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", session.LocationIdFk);
            return View(session);
        }

        /// <summary>
        /// Gets the edit view for a session in the database.
        /// </summary>
        /// <param name="id">The primary key of that session.</param>
        /// <returns>The edit form for the Session if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Sessions/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", session.LocationIdFk);
            return View(session);
        }

        /// <summary>
        /// Edits a given session in the database.
        /// </summary>
        /// <param name="id">The primary key of the session to edit.</param>
        /// <param name="session">The form data.</param>
        /// <returns>Redirect to List if successful, back to the form otherwise.</returns>
        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Sessions/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventType,EventDateTime,LocationIdFk")] Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(session);
                        TempData[TempDataHelper.Success] = "Successfully edited Session with ID " + session.Id; 
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SessionExists(session.Id))
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
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error editing Session: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            ViewData["LocationIdFk"] = new SelectList(_context.Locations, "Id", "Id", session.LocationIdFk);
            return View(session);
        }

        /// <summary>
        /// Gets the deletion confirmation view for a given Session.
        /// </summary>
        /// <param name="id">The primary key for that session.</param>
        /// <returns>The deletion view if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Sessions/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.LocationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        /// <summary>
        /// Deletes a given session in the database.
        /// </summary>
        /// <param name="id">The primary key of that session.</param>
        /// <returns>Redirect to List if successful, Problem if one occurs.</returns>
        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Sessions == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Sessions'  is null.");
                }
                var session = await _context.Sessions.FindAsync(id);
                if (session != null)
                {
                    _context.Sessions.Remove(session);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = "Successfully deleted Session with ID " + id;
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting Session: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
          return (_context.Sessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
