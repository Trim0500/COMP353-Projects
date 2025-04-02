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
    /// Handles all interactions with LogEmails in the database.
    /// </summary>
    public class LogemailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates the controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public LogemailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets base list view for LogEmails.
        /// </summary>
        /// <returns>List view for LogEmails.</returns>
        [HttpGet]
        [Route("Logemails")]
        public async Task<IActionResult> Index()
        {
              return _context.Logemails != null ? 
                          View(await _context.Logemails.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Logemails'  is null.");
        }

        /// <summary>
        /// Gets details for a give LogEmail in the system.
        /// </summary>
        /// <param name="recipient">The email's recipient.</param>
        /// <param name="ddt">The email's delivery datetime.</param>
        /// <returns>Details view if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Logemails/{recipient}/{ddt}")]
        public async Task<IActionResult> Details(string recipient, DateTime ddt)
        {
            if (_context.Logemails == null)
            {
                return NotFound();
            }

            var logemail = await _context.Logemails
                .FirstOrDefaultAsync(m => m.Recipient == recipient && m.DeliveryDateTime == ddt);

            if (logemail == null)
            {
                return NotFound();
            }

            return View(logemail);
        }

        /// <summary>
        /// Gets creation form for a new LogEmail.
        /// </summary>
        /// <returns>Creation form for a new LogEmail.</returns>
        [HttpGet]
        [Route("Logemails/Create")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new LogEmail in the database.
        /// </summary>
        /// <param name="logemail">The form data.</param>
        /// <returns>Redirect to list view if successful, back to form otherwise.</returns>
        // POST: Logemails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Logemails/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Recipient,DeliveryDateTime,Sender,Subject,Body")] Logemail logemail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(logemail);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = String.Format("Successfully created LogEmail {0}/{1}", logemail.Recipient, logemail.DeliveryDateTime);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Error creating LogEmail: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating LogEmail: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            return View(logemail);
        }

        /// <summary>
        /// Gets the edit view for a given LogEmail record.
        /// </summary>
        /// <param name="recipient">The LogEmail's recipient.</param>
        /// <param name="ddt">Its delivery datetime.</param>
        /// <returns>The edit form if it exists, 404 otherwise.</returns>
        [HttpGet]
        [Route("Logemails/{recipient}/{ddt}/Edit")]
        public async Task<IActionResult> Edit(string recipient, DateTime ddt)
        {
            if (_context.Logemails == null)
            {
                return NotFound();
            }

            var logemail = await _context.Logemails
                .FirstOrDefaultAsync(m => m.Recipient == recipient && m.DeliveryDateTime == ddt);

            if (logemail == null)
            {
                return NotFound();
            }
            return View(logemail);
        }

        /// <summary>
        /// Edits a given LogEmail in the system.
        /// </summary>
        /// <param name="logemail">The form data.</param>
        /// <returns>Redirect to List view if successful, 404 if not found, back to form if error.</returns>
        // POST: Logemails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Logemails/{recipient}/{ddt}/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Recipient,DeliveryDateTime,Sender,Subject,Body")] Logemail logemail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(logemail);
                        await _context.SaveChangesAsync();
                        TempData[TempDataHelper.Success] = String.Format("Successfully edited LogEmail {0}/{1}", logemail.Recipient, logemail.DeliveryDateTime);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LogemailExists(logemail.Recipient))
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
                    TempData[TempDataHelper.Error] = "Error editing LogEmail: State invalid";
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error editing LogEmail: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return View(logemail);
        }

        /// <summary>
        /// Gets the deletion view for a given LogEmail.
        /// </summary>
        /// <param name="recipient">The email's recipient.</param>
        /// <param name="ddt">The email's delivery datetime.</param>
        /// <returns>The deletion confirmation view if it exists, 404 if not.</returns>
        [HttpGet]
        [Route("Logemails/{recipient}/{ddt}/Delete")]
        public async Task<IActionResult> Delete(string recipient, DateTime ddt)
        {
            if (_context.Logemails == null)
            {
                return NotFound();
            }

            var logemail = await _context.Logemails
                .FirstOrDefaultAsync(m => m.Recipient == recipient && m.DeliveryDateTime == ddt);

            if (logemail == null)
            {
                return NotFound();
            }

            return View(logemail);
        }

        /// <summary>
        /// Deletes a given LogEmail in the system.
        /// </summary>
        /// <param name="recipient">The email's recipient.</param>
        /// <param name="ddt">The email's delivery datetime.</param>
        /// <returns>Redirect to list view if successful, Problem if one occurs.</returns>
        [HttpPost, ActionName("Delete")]
        [Route("Logemails/{recipient}/{ddt}/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string recipient, DateTime ddt)
        {
            try
            {
                if (_context.Logemails == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Logemails'  is null.");
                }

                var logemail = await _context.Logemails
                    .FirstOrDefaultAsync(m => m.Recipient == recipient && m.DeliveryDateTime == ddt);

                if (logemail != null)
                {
                    _context.Logemails.Remove(logemail);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = String.Format("Successfully deleted LogEmail {0}/{1}", logemail.Recipient, logemail.DeliveryDateTime);
            }
            catch (Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting LogEmail: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LogemailExists(string id)
        {
          return (_context.Logemails?.Any(e => e.Recipient == id)).GetValueOrDefault();
        }
    }
}
