using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class CustomerType
    {

        public CustomerType()
        {

            Customers = new HashSet<Customer>();

        }

        public int CustomerTypeId { get; set; }

        [Required(ErrorMessage = "A customer type name is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50, ErrorMessage = "A valid customer type up to 50 characters")]
        public string CustomerTypeName { get; set; } = null!;

        #region Navigation Properties
        public virtual ICollection<Customer> Customers { get; set; }

        #endregion

    }
}
