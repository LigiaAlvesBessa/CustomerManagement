using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class Payment
    {

        public int PaymentId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public int? ServiceId { get; set; }

        [Required]
        public int PaymentStatusId { get; set; }

        [Required(ErrorMessage = "A payday is required")]
        [Column(TypeName = "date")]
        public DateTime PayDay { get; set; }

        [Required(ErrorMessage = "An amount paid is required")]
        public decimal AmountPaid { get; set; }

        #region Navigation Properties
        public virtual Customer? Customer { get; set; } = null!;
        public virtual Service? Service { get; set; } = null!;
        public virtual PaymentStatus? PaymentStatus { get; set; } = null!;

        #endregion

    }
}
