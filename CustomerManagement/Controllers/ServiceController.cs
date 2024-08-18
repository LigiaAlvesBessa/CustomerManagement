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
    public class ServiceController : Controller
    {
        private readonly CustomerManagement_DBContext _context;

        public ServiceController(CustomerManagement_DBContext context)
        {
            _context = context;
        }

        // GET: Service
        public async Task<IActionResult> Index()
        {
              return _context.Service != null ? 
                          View(await _context.Service.ToListAsync()) :
                          Problem("Entity set 'CustomerManagement_DBContext.Service'  is null.");
        }

        // GET: Service/Details/5
        public async Task<IActionResult> Details(int? appointmentId)
        {
            var appointment = await _context.Appointment
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

            if (appointment == null || appointment.Service == null)
            {
                return NotFound();
            }

            return View(appointment.Service);
        }

        public async Task<IActionResult> DetailsPayment(int? paymentId)
        {
            var payment = await _context.Payment
                .Include(p => p.Service)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);

            if (payment == null || payment.Service == null)
            {
                return NotFound();
            }

            return View(payment.Service);
        }

        // GET: Service/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,ServiceName,ServicePrice")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Service/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Service == null)
            {
                return NotFound();
            }

            var service = await _context.Service.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,ServiceName,ServicePrice")] Service service)
        {
            if (id != service.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceId))
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
            return View(service);
        }

        // GET: Service/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Service == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Service == null)
            {
                return Problem("Entity set 'CustomerManagement_DBContext.Service'  is null.");
            }
            var service = await _context.Service.FindAsync(id);
            if (service != null)
            {
                _context.Service.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
          return (_context.Service?.Any(e => e.ServiceId == id)).GetValueOrDefault();
        }
    }
}
