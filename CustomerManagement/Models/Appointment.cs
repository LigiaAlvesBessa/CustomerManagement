using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class Appointment
    {

        public int AppointmentId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }

        [Required]
        public int AppointmentStatusId { get; set; }

        [Required(ErrorMessage = "An appointment date is required")]
        [Column(TypeName = "date")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "An appointment hour is required")]
        [Column(TypeName = "time")]
        public TimeSpan AppointmentHour { get; set; }

        #region Navigation Properties
        public virtual Customer? Customer { get; set; } = null!;
        public virtual Service? Service { get; set; } = null!;
        public virtual Employee? Employee { get; set; } = null!;
        public virtual AppointmentStatus? AppointmentStatus { get; set; } = null!;

        #endregion

    }
}
