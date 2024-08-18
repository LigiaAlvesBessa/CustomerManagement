using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerManagement.Models;

namespace CustomerManagement.Controllers
{
    public class BankDataController : Controller
    {
        private readonly CustomerManagement_DBContext _context;

        public BankDataController(CustomerManagement_DBContext context)
        {
            _context = context;
        }

        // GET: BankData
        public async Task<IActionResult> Index()
        {
              return _context.BankData != null ? 
                          View(await _context.BankData.ToListAsync()) :
                          Problem("Entity set 'CustomerManagement_DBContext.BankData'  is null.");
        }

        // GET: BankData/Details/5
        public async Task<IActionResult> Details(int? customerId)
        {
            var customer = await _context.Customer
                .Include(c => c.BankData)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null || customer.BankData == null)
            {
                return NotFound();
            }

            return View(customer.BankData);
        }

        // GET: BankData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BankDataId,Iban,BankName")] BankData bankData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankData);
        }

        // GET: BankData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BankData == null)
            {
                return NotFound();
            }

            var bankData = await _context.BankData.FindAsync(id);
            if (bankData == null)
            {
                return NotFound();
            }
            return View(bankData);
        }

        // POST: BankData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BankDataId,Iban,BankName")] BankData bankData)
        {
            if (id != bankData.BankDataId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankDataExists(bankData.BankDataId))
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
            return View(bankData);
        }

        // GET: BankData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BankData == null)
            {
                return NotFound();
            }

            var bankData = await _context.BankData
                .FirstOrDefaultAsync(m => m.BankDataId == id);
            if (bankData == null)
            {
                return NotFound();
            }

            return View(bankData);
        }

        // POST: BankData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BankData == null)
            {
                return Problem("Entity set 'CustomerManagement_DBContext.BankData'  is null.");
            }
            var bankData = await _context.BankData.FindAsync(id);
            if (bankData != null)
            {
                _context.BankData.Remove(bankData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankDataExists(int id)
        {
          return (_context.BankData?.Any(e => e.BankDataId == id)).GetValueOrDefault();
        }
    }
}
