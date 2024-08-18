using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class Customer
    {

        public Customer()
        {

            Appointments = new HashSet<Appointment>();
            Payments = new HashSet<Payment>();

        }

        public int CustomerId { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        public int CustomerTypeId { get; set; }

        [Required]
        public int BankDataId { get; set; }

        [Required]
        public int LoginId { get; set; }

        [Required(ErrorMessage = "A customer name is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(100, ErrorMessage = "A valid customer name up to 100 characters")]
        public string CustomerName { get; set; } = null!;

        [Required(ErrorMessage = "A customer birthday is required")]
        [Column(TypeName = "date")]
        public DateTime CustomerBirthday { get; set; }

        [Required(ErrorMessage = "A customer phone number is required")]
        [Column(TypeName = "char")]
        [StringLength(9, ErrorMessage = "A valid customer phone number with 9 characters")]
        public string CustomerPhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "A customer email is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(256, ErrorMessage = "A valid customer email up to 256 characters")]
        public string CustomerEmail { get; set; } = null!;

        [Required(ErrorMessage = "A customer NIF is required")]
        [Column(TypeName = "char")]
        [StringLength(9, ErrorMessage = "A valid customer NIF with 9 characters")]
        public string CustomerNIF { get; set; } = null!;

        public decimal? MonthlyPayment { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        #region Navigation Properties

        public virtual Address? Address { get; set; } = null!;
        public virtual CustomerType? CustomerType { get; set; } = null!;
        public virtual BankData? BankData { get; set; } = null!;
        public virtual Login? Login { get; set; } = null!;

        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }

        #endregion

    }
}
