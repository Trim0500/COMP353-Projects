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
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payments.Include(p => p.CmnFkNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
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

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["CmnFk"] = new SelectList(_context.Clubmembers, "Cmn", "Cmn");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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

        // GET: Payments/Edit/5
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

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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

        // GET: Payments/Delete/5
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

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
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
