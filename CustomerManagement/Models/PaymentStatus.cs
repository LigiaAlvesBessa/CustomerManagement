using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class PaymentStatus
    {

        public PaymentStatus()
        {

            Payments = new HashSet<Payment>();

        }

        public int PaymentStatusId { get; set; }

        [Required(ErrorMessage = "A payment status name is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(20, ErrorMessage = "A valid payment status name up to 20 characters")]
        public string PaymentStatusName { get; set; } = null!;

        #region Navigation Properties
        public virtual ICollection<Payment> Payments { get; set; }

        #endregion

    }
}
