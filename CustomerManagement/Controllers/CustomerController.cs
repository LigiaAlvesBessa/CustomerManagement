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
    public class CustomerController : Controller
    {
        private readonly CustomerManagement_DBContext _context;

        public CustomerController(CustomerManagement_DBContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var customerManagement_DBContext = _context.Customer.Include(c => c.Address).Include(c => c.BankData).Include(c => c.CustomerType).Include(c => c.Login);
            return View(await customerManagement_DBContext.ToListAsync());
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.Address)
                .Include(c => c.BankData)
                .Include(c => c.CustomerType)
                .Include(c => c.Login)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/DetailsAppointment/5
        public async Task<IActionResult> DetailsAppointment(int? appointmentId)
        {

            var appointment = await _context.Appointment
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

            if (appointment == null || appointment.Customer == null)
            {
                return NotFound();
            }

            return View(appointment.Customer);
        }

        public async Task<IActionResult> DetailsPayment(int? paymentId)
        {
            var payment = await _context.Payment
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);

            if (payment == null || payment.Customer == null)
            {
                return NotFound();
            }

            return View(payment.Customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {

            PopulateDropdowns();
            return View();

        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,AddressId,CustomerTypeId,BankDataId,LoginId,CustomerName,CustomerBirthday,CustomerPhoneNumber,CustomerEmail,CustomerNIF,MonthlyPayment,StartDate")] Customer customer)
        {

            if (!ModelState.IsValid)
            {
                // Verificar erros
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Customer.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Success));
            }

            await PopulateDropdownsAsync();
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressName", customer.AddressId);
            ViewData["BankDataId"] = new SelectList(_context.BankData, "BankDataId", "BankName", customer.BankDataId);
            ViewData["CustomerTypeId"] = new SelectList(_context.CustomerType, "CustomerTypeId", "CustomerTypeName", customer.CustomerTypeId);
            ViewData["LoginId"] = new SelectList(_context.Login, "LoginId", "Password", customer.LoginId);
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,AddressId,CustomerTypeId,BankDataId,LoginId,CustomerName,CustomerBirthday,CustomerPhoneNumber,CustomerEmail,CustomerNIF,MonthlyPayment,StartDate")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressName", customer.AddressId);
            ViewData["BankDataId"] = new SelectList(_context.BankData, "BankDataId", "BankName", customer.BankDataId);
            ViewData["CustomerTypeId"] = new SelectList(_context.CustomerType, "CustomerTypeId", "CustomerTypeName", customer.CustomerTypeId);
            ViewData["LoginId"] = new SelectList(_context.Login, "LoginId", "Password", customer.LoginId);
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.Address)
                .Include(c => c.BankData)
                .Include(c => c.CustomerType)
                .Include(c => c.Login)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customer == null)
            {
                return Problem("Entity set 'CustomerManagement_DBContext.Customer'  is null.");
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customer?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }

        private void PopulateDropdowns(Customer customer = null!)
        {
            ViewBag.Addresses = new SelectList(_context.Address.ToList(), "AddressId", "AddressName", customer?.AddressId);
            ViewBag.CustomerTypes = new SelectList(_context.CustomerType.ToList(), "CustomerTypeId", "CustomerTypeName", customer?.CustomerTypeId);
            ViewBag.BankDatas = new SelectList(_context.BankData.ToList(), "BankDataId", "Iban", customer?.BankDataId);
            ViewBag.Logins = new SelectList(_context.Login.ToList(), "LoginId", "UserName", customer?.LoginId);
        }

        private async Task PopulateDropdownsAsync(Customer customer = null!)
        {
            ViewBag.Addresses = new SelectList(await _context.Address.ToListAsync(), "AddressId", "AddressName", customer?.AddressId);
            ViewBag.CustomerTypes = new SelectList(await _context.CustomerType.ToListAsync(), "CustomerTypeId", "CustomerTypeName", customer?.CustomerTypeId);
            ViewBag.BankDatas = new SelectList(await _context.BankData.ToListAsync(), "BankDataId", "Iban", customer?.BankDataId);
            ViewBag.Logins = new SelectList(await _context.Login.ToListAsync(), "LoginId", "UserName", customer?.LoginId);
        }

        public IActionResult Success()
        {
            return View();
        }

        // GET: Customers/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: Customers/Search
        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View(); // Retorna a view de pesquisa sem resultados
            }

            var customer = await _context.Customer
                .Where(c => c.CustomerName.Contains(searchTerm) || c.CustomerEmail.Contains(searchTerm))
                .ToListAsync();

            if (!customer.Any())
            {
                ViewData["Message"] = "No customer found.";
                return View(); // Retorna a view de pesquisa com mensagem de "nenhum resultado"
            }

            return View("SearchResults", customer);
        }

    }
}
