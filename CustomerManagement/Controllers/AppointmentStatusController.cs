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
    public class AppointmentStatusController : Controller
    {
        private readonly CustomerManagement_DBContext _context;

        public AppointmentStatusController(CustomerManagement_DBContext context)
        {
            _context = context;
        }

        // GET: AppointmentStatus
        public async Task<IActionResult> Index()
        {
              return _context.AppointmentStatus != null ? 
                          View(await _context.AppointmentStatus.ToListAsync()) :
                          Problem("Entity set 'CustomerManagement_DBContext.AppointmentStatus'  is null.");
        }

        // GET: AppointmentStatus/Details/5
        public async Task<IActionResult> Details(int? appointmentId)
        {

            var appointment = await _context.Appointment
                .Include(a => a.AppointmentStatus)
                .FirstOrDefaultAsync(a=>a.AppointmentId == appointmentId);

            if (appointment == null || appointment.AppointmentStatus == null)
            {
                return NotFound();
            }

            return View(appointment.AppointmentStatus);
        }


        // GET: AppointmentStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppointmentStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentStatusId,AppointmentStatusName")] AppointmentStatus appointmentStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointmentStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointmentStatus);
        }

        // GET: AppointmentStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentStatus = await _context.AppointmentStatus.FindAsync(id);
            if (appointmentStatus == null)
            {
                return NotFound();
            }
            return View(appointmentStatus);
        }

        // POST: AppointmentStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentStatusId,AppointmentStatusName")] AppointmentStatus appointmentStatus)
        {
            if (id != appointmentStatus.AppointmentStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentStatusExists(appointmentStatus.AppointmentStatusId))
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
            return View(appointmentStatus);
        }

        // GET: AppointmentStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AppointmentStatus == null)
            {
                return NotFound();
            }

            var appointmentStatus = await _context.AppointmentStatus
                .FirstOrDefaultAsync(m => m.AppointmentStatusId == id);
            if (appointmentStatus == null)
            {
                return NotFound();
            }

            return View(appointmentStatus);
        }

        // POST: AppointmentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AppointmentStatus == null)
            {
                return Problem("Entity set 'CustomerManagement_DBContext.AppointmentStatus'  is null.");
            }
            var appointmentStatus = await _context.AppointmentStatus.FindAsync(id);
            if (appointmentStatus != null)
            {
                _context.AppointmentStatus.Remove(appointmentStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentStatusExists(int id)
        {
          return (_context.AppointmentStatus?.Any(e => e.AppointmentStatusId == id)).GetValueOrDefault();
        }
    }
}
