using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class BankData
    {

        public BankData()
        {

            Customers = new HashSet<Customer>();

        }

        public int BankDataId { get; set; }

        [Required(ErrorMessage = "An iban is required")]
        [Column(TypeName = "char")]
        [StringLength(25, ErrorMessage = "A valid iban with 25 characters and start with PT50")]
        public string Iban { get; set; } = null!;

        [Required(ErrorMessage = "A bank name is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(100, ErrorMessage = "A valid bank name up to 100 characters")]
        public string BankName { get; set; }= null!;

        #region Navigation Properties
        public virtual ICollection<Customer> Customers { get; set; }

        #endregion

    }
}
