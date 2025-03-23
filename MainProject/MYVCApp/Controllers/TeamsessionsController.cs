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
    public class TeamsessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teamsessions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teamsessions.Include(t => t.SessionIdFkNavigation).Include(t => t.TeamFormationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

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

        // GET: Teamsessions/Create
        [HttpGet]
        [Route("Teamsessions/Create")]
        public IActionResult Create()
        {
            ViewData["SessionIdFk"] = new SelectList(_context.Sessions, "Id", "Id");
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id");
            return View();
        }

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
