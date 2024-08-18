using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class Employee
    {

        public Employee()
        {

            Appointments = new HashSet<Appointment>();

        }

        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "An employee name is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(100, ErrorMessage = "A valid employee name up to 100 characters")]
        public string EmployeeName { get; set; } = null!;

        [Required(ErrorMessage = "An employee phone number is required")]
        [Column(TypeName = "char")]
        [StringLength(9, ErrorMessage = "A valid employee phone number with 9 characters")]
        public string EmployeePhoneNumber { get; set; } = null!;

        #region Navigation Properties
        public virtual ICollection<Appointment> Appointments { get; set; }

        #endregion

    }
}
