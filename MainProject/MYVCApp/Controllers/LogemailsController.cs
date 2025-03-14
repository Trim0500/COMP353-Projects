using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYVCApp.Contexts;
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

        // GET: Logemails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Logemails == null)
            {
                return NotFound();
            }

            var logemail = await _context.Logemails
                .FirstOrDefaultAsync(m => m.Recipient == id);
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
            if (ModelState.IsValid)
            {
                _context.Add(logemail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logemail);
        }

        // GET: Logemails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Logemails == null)
            {
                return NotFound();
            }

            var logemail = await _context.Logemails.FindAsync(id);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Recipient,DeliveryDateTime,Sender,Subject,Body")] Logemail logemail)
        {
            if (id != logemail.Recipient)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logemail);
                    await _context.SaveChangesAsync();
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
            return View(logemail);
        }

        // GET: Logemails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Logemails == null)
            {
                return NotFound();
            }

            var logemail = await _context.Logemails
                .FirstOrDefaultAsync(m => m.Recipient == id);
            if (logemail == null)
            {
                return NotFound();
            }

            return View(logemail);
        }

        // POST: Logemails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Logemails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Logemails'  is null.");
            }
            var logemail = await _context.Logemails.FindAsync(id);
            if (logemail != null)
            {
                _context.Logemails.Remove(logemail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogemailExists(string id)
        {
          return (_context.Logemails?.Any(e => e.Recipient == id)).GetValueOrDefault();
        }
    }
}
