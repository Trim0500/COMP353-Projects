﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYVCApp.Contexts;
using MYVCApp.Helpers;
using MYVCApp.Models;

namespace MYVCApp.Controllers
{
    /// <summary>
    /// Handles all interactions with TeamMembers in the database.
    /// </summary>
    public class TeammembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public TeammembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets base list view for TeamMembers.
        /// </summary>
        /// <returns>List view for TeamMembers.</returns>
        [HttpGet]
        [Route("Teammembers")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teammembers.Include(t => t.CmnFkNavigation).Include(t => t.TeamFormationIdFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets details view for a given TeamMember.
        /// </summary>
        /// <param name="cmn">The ID of the club member.</param>
        /// <param name="teamformation">The ID of the team formation they're part of.</param>
        /// <returns>The details view for that TeamMember if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Teammembers/{cmn}/{teamformation}")]
        public async Task<IActionResult> Details(int cmn, int teamformation)
        {
            if (cmn < 0 || teamformation < 0 || _context.Teammembers == null)
            {
                return NotFound();
            }

            var teammember = await _context.Teammembers
                .Include(t => t.CmnFkNavigation)
                .Include(t => t.TeamFormationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.TeamFormationIdFk == teamformation && m.CmnFk == cmn);

            if (teammember == null)
            {
                return NotFound();
            }

            return View(teammember);
        }

        /// <summary>
        /// Gets the creation view for a new TeamMember in the database.
        /// </summary>
        /// <returns>Creation form for a new TeamMember.</returns>
        // GET: Teammembers/Create
        [HttpGet]
        [Route("Teammembers/Create")]
        public IActionResult Create()
        {
            ViewData["CmnFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn");
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates a new TeamMember in the database.
        /// </summary>
        /// <param name="teammember">Form data.</param>
        /// <returns>Redirect to index if successful, returns to form otherwise.</returns>
        // POST: Teammembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Teammembers/Create")]
        public async Task<IActionResult> Create([Bind("TeamFormationIdFk,CmnFk,Role,AssignmentDateTime")] Teammember teammember)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> validationResult = await TeamFormationMemberAdditionHelper.Validate(teammember, _context);

                    if (validationResult.Count != 0)
                    {
                        StringBuilder errors = new StringBuilder();

                        errors.AppendLine("Validation error(s): ");
                        foreach(string s in validationResult)
                        {
                            errors.AppendLine(s);
                        }
                        throw new InvalidOperationException(errors.ToString());
                    }

                    _context.Add(teammember);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = String.Format("Successfully created TeamMember {0}/{1}", teammember.CmnFk, teammember.TeamFormationIdFk);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Error creating TeamMember: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating TeamMember: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CmnFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", teammember.CmnFk);
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id", teammember.TeamFormationIdFk);
            return View(teammember);
        }

        /// <summary>
        /// Gets Edit view for a given TeamMember.
        /// </summary>
        /// <param name="cmn">The ID of the club member.</param>
        /// <param name="teamformation">The ID of the team formation they're part of.</param>
        /// <returns>The Edit view for that TeamMember if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Teammembers/{cmn}/{teamformation}/Edit")]
        public async Task<IActionResult> Edit(int cmn, int teamformation)
        {
            if (cmn < 0 || teamformation < 0 || _context.Teammembers == null)
            {
                return NotFound();
            }

            var teammember = await _context.Teammembers
                .Include(t => t.CmnFkNavigation)
                .Include(t => t.TeamFormationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.TeamFormationIdFk == teamformation && m.CmnFk == cmn);

            if (teammember == null)
            {
                return NotFound();
            }
            ViewData["CmnFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", teammember.CmnFk);
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id", teammember.TeamFormationIdFk);
            return View(teammember);
        }

        /// <summary>
        /// Edits a TeamMember in the database.
        /// </summary>
        /// <param name="teammember">Form data.</param>
        /// <returns>Redirect to index if successful, returns to form otherwise.</returns>
        // POST: Teammembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Teammembers/{cmn}/{teamformation}/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("TeamFormationIdFk,CmnFk,Role,AssignmentDateTime")] Teammember teammember)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(teammember);
                        await _context.SaveChangesAsync();
                        TempData[TempDataHelper.Success] = String.Format("Sucessfully edited TeamMember {0}/{1}", teammember.CmnFk, teammember.TeamFormationIdFk);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TeammemberExists(teammember.TeamFormationIdFk))
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
                    TempData[TempDataHelper.Error] = "Error editing TeamMember: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error editing TeamMember: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CmnFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", teammember.CmnFk);
            ViewData["TeamFormationIdFk"] = new SelectList(_context.Teamformations, "Id", "Id", teammember.TeamFormationIdFk);
            return View(teammember);
        }

        /// <summary>
        /// Gets deletion confirmation view for a given TeamMember.
        /// </summary>
        /// <param name="cmn">The ID of the club member.</param>
        /// <param name="teamformation">The ID of the team formation they're part of.</param>
        /// <returns>The deletion confirmation view for that TeamMember if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Teammembers/{cmn}/{teamformation}/Delete")]
        public async Task<IActionResult> Delete(int cmn, int teamformation)
        {
            if (cmn < 0 || teamformation < 0 || _context.Teammembers == null)
            {
                return NotFound();
            }

            var teammember = await _context.Teammembers
                .Include(t => t.CmnFkNavigation)
                .Include(t => t.TeamFormationIdFkNavigation)
                .FirstOrDefaultAsync(m => m.TeamFormationIdFk == teamformation && m.CmnFk == cmn);

            if (teammember == null)
            {
                return NotFound();
            }

            return View(teammember);
        }

        /// <summary>
        /// Edits a TeamMember in the database.
        /// </summary>
        /// <param name="cmn">The ID of the club member.</param>
        /// <param name="teamformation">The ID of the team they're part of.</param>
        /// <returns>Redirect to index if successful, Problem if one occurs..</returns>
        [HttpPost, ActionName("Delete")]
        [Route("Teammembers/{cmn}/{teamformation}/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int cmn, int teamformation)
        {
            try
            {
                if (_context.Teammembers == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Teammembers'  is null.");
                }
                var teammember = await _context.Teammembers
                    .Include(t => t.CmnFkNavigation)
                    .Include(t => t.TeamFormationIdFkNavigation)
                    .FirstOrDefaultAsync(m => m.TeamFormationIdFk == teamformation && m.CmnFk == cmn);

                if (teammember != null)
                {
                    _context.Teammembers.Remove(teammember);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = String.Format("Sucessfully deleted TeamMember {0}/{1}", teammember.CmnFk, teammember.TeamFormationIdFk);
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting TeamMember: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TeammemberExists(int id)
        {
          return (_context.Teammembers?.Any(e => e.TeamFormationIdFk == id)).GetValueOrDefault();
        }
    }
}
