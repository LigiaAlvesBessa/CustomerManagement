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
    public class CustomerTypeController : Controller
    {
        private readonly CustomerManagement_DBContext _context;

        public CustomerTypeController(CustomerManagement_DBContext context)
        {
            _context = context;
        }

        // GET: CustomerType
        public async Task<IActionResult> Index()
        {
              return _context.CustomerType != null ? 
                          View(await _context.CustomerType.ToListAsync()) :
                          Problem("Entity set 'CustomerManagement_DBContext.CustomerType'  is null.");
        }

        // GET: CustomerType/Details/5
        public async Task<IActionResult> Details(int? customerId)
        {
            var customer = await _context.Customer
                .Include(c => c.CustomerType)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null || customer.CustomerType == null)
            {
                return NotFound();
            }

            return View(customer.CustomerType);
        }

        // GET: CustomerType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerTypeId,CustomerTypeName")] CustomerType customerType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerType);
        }

        // GET: CustomerType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerType == null)
            {
                return NotFound();
            }

            var customerType = await _context.CustomerType.FindAsync(id);
            if (customerType == null)
            {
                return NotFound();
            }
            return View(customerType);
        }

        // POST: CustomerType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerTypeId,CustomerTypeName")] CustomerType customerType)
        {
            if (id != customerType.CustomerTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerTypeExists(customerType.CustomerTypeId))
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
            return View(customerType);
        }

        // GET: CustomerType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerType == null)
            {
                return NotFound();
            }

            var customerType = await _context.CustomerType
                .FirstOrDefaultAsync(m => m.CustomerTypeId == id);
            if (customerType == null)
            {
                return NotFound();
            }

            return View(customerType);
        }

        // POST: CustomerType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerType == null)
            {
                return Problem("Entity set 'CustomerManagement_DBContext.CustomerType'  is null.");
            }
            var customerType = await _context.CustomerType.FindAsync(id);
            if (customerType != null)
            {
                _context.CustomerType.Remove(customerType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerTypeExists(int id)
        {
          return (_context.CustomerType?.Any(e => e.CustomerTypeId == id)).GetValueOrDefault();
        }
    }
}
