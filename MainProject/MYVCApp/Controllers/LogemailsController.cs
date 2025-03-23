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
    public class LogemailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogemailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Logemails
        public async Task<IActionResult> Index()
        {
              return _context.Logemails != null ? 
                          View(await _context.Logemails.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Logemails'  is null.");
        }

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

        // GET: Logemails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Logemails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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
