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
    public class PaymentController : Controller
    {
        private readonly CustomerManagement_DBContext _context;

        public PaymentController(CustomerManagement_DBContext context)
        {
            _context = context;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var customerManagement_DBContext = _context.Payment.Include(p => p.Customer).Include(p => p.PaymentStatus).Include(p => p.Service);
            return View(await customerManagement_DBContext.ToListAsync());
        }

        // GET: Payment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Customer)
                .Include(p => p.PaymentStatus)
                .Include(p => p.Service)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,CustomerId,ServiceId,PaymentStatusId,PayDay,AmountPaid")] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Payment.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Success));
            }

            await PopulateDropdownsAsync();
            return View(payment);

        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerEmail", payment.CustomerId);
            ViewData["PaymentStatusId"] = new SelectList(_context.PaymentStatus, "PaymentStatusId", "PaymentStatusName", payment.PaymentStatusId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceName", payment.ServiceId);
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,CustomerId,ServiceId,PaymentStatusId,PayDay,AmountPaid")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerEmail", payment.CustomerId);
            ViewData["PaymentStatusId"] = new SelectList(_context.PaymentStatus, "PaymentStatusId", "PaymentStatusName", payment.PaymentStatusId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceName", payment.ServiceId);
            return View(payment);
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Customer)
                .Include(p => p.PaymentStatus)
                .Include(p => p.Service)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Payment == null)
            {
                return Problem("Entity set 'CustomerManagement_DBContext.Payment'  is null.");
            }
            var payment = await _context.Payment.FindAsync(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
          return (_context.Payment?.Any(e => e.PaymentId == id)).GetValueOrDefault();
        }

        private void PopulateDropdowns(Payment payment = null!)
        {
            ViewBag.Customers = new SelectList(_context.Customer.ToList(), "CustomerId", "CustomerName", payment?.CustomerId);
            ViewBag.Services = new SelectList(_context.Service.ToList(), "ServiceId", "ServiceName", payment?.ServiceId);
            ViewBag.PaymentStatus = new SelectList(_context.PaymentStatus.ToList(), "PaymentStatusId", "PaymentStatusName", payment?.PaymentStatusId);
        }

        private async Task PopulateDropdownsAsync(Payment payment = null!)
        {
            ViewBag.Customers = new SelectList(await _context.Customer.ToListAsync(), "CustomerId", "CustomerName", payment?.CustomerId);
            ViewBag.Services = new SelectList(await _context.Service.ToListAsync(), "ServiceId", "ServiceName", payment?.ServiceId);
            ViewBag.PaymentStatus = new SelectList(await _context.PaymentStatus.ToListAsync(), "PaymentStatusId", "PaymentStatusName", payment?.PaymentStatusId);
        }

        public IActionResult Success()
        {
            return View();
        }

    }
}
