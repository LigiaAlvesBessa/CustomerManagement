using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Models
{
    public partial class CustomerManagement_DBContext : DbContext
    {

        public CustomerManagement_DBContext(DbContextOptions<CustomerManagement_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; } = null!;
        public virtual DbSet<Appointment> Appointment { get; set; } = null!;
        public virtual DbSet<AppointmentStatus> AppointmentStatus { get; set; } = null!;
        public virtual DbSet<BankData> BankData { get; set; } = null!;
        public virtual DbSet<Customer> Customer { get; set; } = null!;
        public virtual DbSet<CustomerType> CustomerType { get; set; } = null!;
        public virtual DbSet<Employee> Employee { get; set; } = null!;
        public virtual DbSet<Login> Login { get; set; } = null!;
        public virtual DbSet<Payment> Payment { get; set; } = null!;
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; } = null!;
        public virtual DbSet<Service> Service { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.AmountPaid)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Customer>()
                .Property(c => c.MonthlyPayment)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Service>()
                .Property(s => s.ServicePrice)
                .HasColumnType("decimal(10,2)");
        }

    }
}
