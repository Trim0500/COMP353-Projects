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
    /// Handles interactions for Payments in the database.
    /// </summary>
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context">Injected DbContext.</param>
        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the base list view for Payment data.
        /// </summary>
        /// <returns>List view for Payment data.</returns>
        [HttpGet]
        [Route("Payments")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payments.Include(p => p.CmnFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets the details for a given payment.
        /// </summary>
        /// <param name="id">The primary key of the payment.</param>
        /// <returns>Details view for that payment if it exists, 404 if not.</returns>
        [HttpGet]
        [Route("Payments/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.CmnFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        /// <summary>
        /// Gets creation form for a new Payment.
        /// </summary>
        /// <returns>Creation form for new payment record.</returns>
        [HttpGet]
        [Route("Payments/Create")]
        public IActionResult Create()
        {
            ViewData["CmnFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn");
            return View();
        }

        /// <summary>
        /// Creates a new Payment record in the database.
        /// </summary>
        /// <param name="payment">The form data.</param>
        /// <returns>Redirect to list view if successful, back to form if not.</returns>
        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Payments/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,PaymentDate,EffectiveDate,Method,CmnFk")] Payment payment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(payment);
                    await _context.SaveChangesAsync();
                    TempData[TempDataHelper.Success] = "Successfully created Payment with ID " + payment.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData[TempDataHelper.Error] = "Error creating Payment: State invalid";
                }
                ViewData["CmnFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", payment.CmnFk);
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error creating Payment: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }

            return View(payment);
        }

        /// <summary>
        /// Gets the edit view for a given payment in the system.
        /// </summary>
        /// <param name="id">The primary key of the payment.</param>
        /// <returns>The edit form if it exists, 404 if not.</returns>
        [HttpGet]
        [Route("Payments/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["CmnFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", payment.CmnFk);
            return View(payment);
        }

        /// <summary>
        /// Edits a given payment record in the database.
        /// </summary>
        /// <param name="id">The primary key of the payment.</param>
        /// <param name="payment">The form data.</param>
        /// <returns>Redirect to List view if successful, 404 if not found, back to form if error.</returns>
        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Payments/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,PaymentDate,EffectiveDate,Method,CmnFk")] Payment payment)
        {
            try
            {
                if (id != payment.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(payment);
                        await _context.SaveChangesAsync();
                        TempData[TempDataHelper.Success] = "Successfully edited Payment with ID " + payment.Id;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PaymentExists(payment.Id))
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
                    TempData[TempDataHelper.Error] = "Error updating Payment: State invalid";
                }
                ViewData["CmnFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn", payment.CmnFk);
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error editing Payment: " + ExceptionFormatter.GetFullMessage(ex);
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        /// <summary>
        /// Gets the deletion confirmation view for a payment.
        /// </summary>
        /// <param name="id">The primary key of the payment.</param>
        /// <returns>The deletion confirmation view if it exists, 404 if not.</returns>
        [HttpGet]
        [Route("Payments/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.CmnFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        /// <summary>
        /// Deletes a given payment in the database.
        /// </summary>
        /// <param name="id">The primary key of the payment.</param>
        /// <returns>Redirect to List view if successful, Problem if one occurs.</returns>
        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Payments/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                if (_context.Payments == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Payments'  is null.");
                }
                var payment = await _context.Payments.FindAsync(id);
                if (payment != null)
                {
                    _context.Payments.Remove(payment);
                }

                await _context.SaveChangesAsync();
                TempData[TempDataHelper.Success] = "Successfully deleted Payment with ID " + payment.Id;
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error deleting Payment: " + ExceptionFormatter.GetFullMessage(ex);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
          return (_context.Payments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
