using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class Login
    {

        public Login()
        {

            Customers = new HashSet<Customer>();

        }

        public int LoginId { get; set; }

        [Required(ErrorMessage = "An username is required")]
        [Column(TypeName = "char")]
        [StringLength(5, ErrorMessage = "A valid username with 5 characters")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "A password is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(12, ErrorMessage = "A valid password up to 12 characters")]
        public string Password { get; set; } = null!;

        #region Navigation Properties
        public virtual ICollection<Customer> Customers { get; set; }

        #endregion

    }
}
