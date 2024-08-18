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
    public class AppointmentController : Controller
    {
        private readonly CustomerManagement_DBContext _context;

        public AppointmentController(CustomerManagement_DBContext context)
        {
            _context = context;
        }

        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            var customerManagement_DBContext = _context.Appointment.Include(a => a.AppointmentStatus).Include(a => a.Customer).Include(a => a.Employee).Include(a => a.Service);
            return View(await customerManagement_DBContext.ToListAsync());
        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.AppointmentStatus)
                .Include(a => a.Customer)
                .Include(a => a.Employee)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,CustomerId,ServiceId,EmployeeId,AppointmentStatusId,AppointmentDate,AppointmentHour")] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                // Verificar erros
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                // Repreenche os dropdowns em caso de falha de validação
                PopulateDropdowns();
                return View(appointment);
            }

            try
            {
                _context.Appointment.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log e tratamento de exceção aqui
                Console.WriteLine(ex.Message);
                // Repreenche os dropdowns em caso de falha ao salvar
                PopulateDropdowns();
                return View(appointment);
            }
        }
        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["AppointmentStatusId"] = new SelectList(_context.AppointmentStatus, "AppointmentStatusId", "AppointmentStatusName", appointment.AppointmentStatusId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerEmail", appointment.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeName", appointment.EmployeeId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceName", appointment.ServiceId);
            return View(appointment);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,CustomerId,ServiceId,EmployeeId,AppointmentStatusId,AppointmentDate,AppointmentHour")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["AppointmentStatusId"] = new SelectList(_context.AppointmentStatus, "AppointmentStatusId", "AppointmentStatusName", appointment.AppointmentStatusId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerEmail", appointment.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeName", appointment.EmployeeId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceName", appointment.ServiceId);
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.AppointmentStatus)
                .Include(a => a.Customer)
                .Include(a => a.Employee)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointment == null)
            {
                return Problem("Entity set 'CustomerManagement_DBContext.Appointment'  is null.");
            }
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }

        private void PopulateDropdowns(Appointment appointment = null!)
        {
            ViewBag.AppointmentStatus = new SelectList(_context.AppointmentStatus.ToList(), "AppointmentStatusId", "AppointmentStatusName", appointment?.AppointmentStatusId);
            ViewBag.Customers = new SelectList(_context.Customer.ToList(), "CustomerId", "CustomerName", appointment?.CustomerId);
            ViewBag.Employees = new SelectList(_context.Employee.ToList(), "EmployeeId", "EmployeeName", appointment?.EmployeeId);
            ViewBag.Services = new SelectList(_context.Service.ToList(), "ServiceId", "ServiceName", appointment?.ServiceId);
        }

        private async Task PopulateDropdownsasync(Appointment appointment = null!)
        {

            ViewBag.AppointmentStatus = new SelectList(await _context.AppointmentStatus.ToListAsync(), "AppointmentStatusId", "AppointmentStatusName", appointment?.AppointmentStatusId);
            ViewBag.Customers = new SelectList(await _context.Customer.ToListAsync(), "CustomerId", "CustomerName", appointment?.CustomerId);
            ViewBag.Employees = new SelectList(await _context.Employee.ToListAsync(), "EmployeeId", "EmployeeName", appointment?.EmployeeId);
            ViewBag.Services = new SelectList(await _context.Service.ToListAsync(), "ServiceId", "ServiceName", appointment?.ServiceId);

        }

    }
}
