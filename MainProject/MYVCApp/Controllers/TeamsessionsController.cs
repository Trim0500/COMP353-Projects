using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using MYVCApp.Contexts;
using MYVCApp.Helpers;
using MYVCApp.Models;

namespace MYVCApp.Controllers
{
    /// <summary>
    /// Handles all TeamSession interactions in the database.
    /// </summary>
    public class TeamsessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public TeamsessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets base list view for TeamSessions.
        /// </summary>
        /// <returns>List view for TeamSessions.</returns>
        [HttpGet]
        [Route("Teamsessions")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teamsessions.Include(t => t.SessionIdFkNavigation).Include(t => t.TeamFormationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets the details view for a given TeamSession.
        /// </summary>
        /// <param name="teamformation">The ID FK of the team formation.</param>
        /// <param name="session">The ID FK for the session.</param>
        /// <returns>The details view for the TeamSession if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Teamsessions/{teamformation}/{session}")]
        public async Task<IActionResult> Details(int teamformation, int session)
        {
            if (session < 0 || teamformation < 0 || _context.Teamsessions == null)
            {
                return NotFound();
            }

            var teamsession = await _context.Teamsessions
                .Include(t => t.SessionIdFkNavigation)
                .Include(t => t.TeamFormationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.TeamFormationIdFk == teamformation && m.SessionIdFk == session);
            
            if (teamsession == null)
            {
                return NotFound();
            }

            return View(teamsession);
        }

        /// <summary>
        /// Gets the creation form for a new TeamSession.
        /// </summary>
        /// <returns>Creation form for a new TeamSession.</returns>
        // GET: Teamsessions/Create
        [HttpGet]
        [Route("Teamsessions/Create")]
        public IActionResult Create()
        {
            ViewData["SessionIdFk"] = new SelectList(_context.Sessions, "Id", "Id");
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates a new TeamSession in the database.
        /// </summary>
        /// <param name="teamsession">The form data.</param>
        /// <returns>Redirect to index if successful, back to form otherwise.</returns>
        // POST: Teamsessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Teamsessions/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamFormationIdFk,SessionIdFk,Score")] Teamsession teamsession)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(teamsession);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = String.Format("Successfully created TeamSession {0}/{1}", teamsession.TeamFormationIdFk, teamsession.SessionIdFk);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Error creating TeamSession: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating TeamSession: " + ExceptionFormatter.GetFullMessage(ex); 
                return RedirectToAction(nameof(Index));
            }

            ViewData["SessionIdFk"] = new SelectList(_context.Sessions, "Id", "Id", teamsession.SessionIdFk);
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id", teamsession.TeamFormationIdFk);
            return View(teamsession);
        }

        /// <summary>
        /// Gets the edit view for a given TeamSession.
        /// </summary>
        /// <param name="teamformation">The ID FK of the team formation.</param>
        /// <param name="session">The ID FK for the session.</param>
        /// <returns>The edit view for the TeamSession if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Teamsessions/{teamformation}/{session}/Edit")]
        public async Task<IActionResult> Edit(int teamformation, int session)
        {
            if (teamformation < 0 || session < 0 || _context.Teamsessions == null)
            {
                return NotFound();
            }

            var teamsession = await _context.Teamsessions
                .Include(t => t.SessionIdFkNavigation)
                .Include(t => t.TeamFormationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.TeamFormationIdFk == teamformation && m.SessionIdFk == session);

            if (teamsession == null)
            {
                return NotFound();
            }
            ViewData["SessionIdFk"] = new SelectList(_context.Sessions, "Id", "Id", teamsession.SessionIdFk);
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id", teamsession.TeamFormationIdFk);
            return View(teamsession);
        }

        /// <summary>
        /// Edits a given TeamSession in the database.
        /// </summary>
        /// <param name="teamsession">The form data.</param>
        /// <returns>Redirects to index if successful, 404 if not found, back to form if some error occured.</returns>
        // POST: Teamsessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Teamsessions/{teamformation}/{session}/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("TeamFormationIdFk,SessionIdFk,Score")] Teamsession teamsession)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(teamsession);
                        await _context.SaveChangesAsync();
                        TempData[TempDataHelper.Success] = String.Format("Successfully edited TeamSession {0}/{1}", teamsession.TeamFormationIdFk, teamsession.SessionIdFk);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TeamsessionExists(teamsession.TeamFormationIdFk))
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
                    TempData[TempDataHelper.Error] = "Error editing TeamSession: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error editing TeamSession: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            ViewData["SessionIdFk"] = new SelectList(_context.Sessions, "Id", "Id", teamsession.SessionIdFk);
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id", teamsession.TeamFormationIdFk);
            return View(teamsession);
        }

        /// <summary>
        /// Gets the deletion confirmation view for a given TeamSession.
        /// </summary>
        /// <param name="teamformation">The ID FK of the TeamFormation</param>
        /// <param name="session">The ID FK of the Session</param>
        /// <returns>Deletion confirmation view if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Teamsessions/{teamformation}/{session}/Delete")]
        public async Task<IActionResult> Delete(int teamformation, int session)
        {
            if (teamformation < 0 || session < 0 || _context.Teamsessions == null)
            {
                return NotFound();
            }

            var teamsession = await _context.Teamsessions
                .Include(t => t.SessionIdFkNavigation)
                .Include(t => t.TeamFormationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.TeamFormationIdFk == teamformation && m.SessionIdFk == session);

            if (teamsession == null)
            {
                return NotFound();
            }

            return View(teamsession);
        }

        /// <summary>
        /// Deletes a given TeamSession in the database.
        /// </summary>
        /// <param name="teamformation">The ID FK of the TeamFormation</param>
        /// <param name="session">The ID FK of the Session</param>
        /// <returns>Redirect to index if successful, Problem if one occured.</returns>
        [HttpPost, ActionName("Delete")]
        [Route("Teamsessions/{teamformation}/{session}/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int teamformation, int session)
        {
            try
            {
                if (_context.Teamsessions == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Teamsessions'  is null.");
                }

                var teamsession = await _context.Teamsessions
                    .Include(t => t.SessionIdFkNavigation)
                    .Include(t => t.TeamFormationIdFkNavigation)
                    .FirstOrDefaultAsync(m => m.TeamFormationIdFk == teamformation && m.SessionIdFk == session);

                if (teamsession != null)
                {
                    _context.Teamsessions.Remove(teamsession);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = String.Format("Successfully deleted TeamSession {0}/{1}", teamsession.TeamFormationIdFk, teamsession.SessionIdFk);
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting TeamSession: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TeamsessionExists(int id)
        {
          return (_context.Teamsessions?.Any(e => e.TeamFormationIdFk == id)).GetValueOrDefault();
        }
    }
}
