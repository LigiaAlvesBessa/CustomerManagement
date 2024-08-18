using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class AppointmentStatus
    {

        public AppointmentStatus()
        {

            Appointments = new HashSet<Appointment>();

        }

        public int AppointmentStatusId { get; set; }

        [Required(ErrorMessage = "An appointment status name is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(20, ErrorMessage = "A valid appointment status name up to 20 characters")]
        public string AppointmentStatusName { get; set; } = null!;

        #region Navigation Properties
        public virtual ICollection<Appointment> Appointments { get; set; }

        #endregion

    }
}
