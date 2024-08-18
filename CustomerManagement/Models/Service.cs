using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class Service
    {

        public Service()
        {

            Appointments = new HashSet<Appointment>();
            Payments = new HashSet<Payment>();

        }

        public int ServiceId { get; set; }

        [Required(ErrorMessage = "A service name is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(100, ErrorMessage = "A valid service name up to 100 characters")]
        public string ServiceName { get; set; } = null!;

        [Required(ErrorMessage = "A service price is required")]
        public decimal ServicePrice { get; set; }

        #region Navigation Properties
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        #endregion

    }
}
