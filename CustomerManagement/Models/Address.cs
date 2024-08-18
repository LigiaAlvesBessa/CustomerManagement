using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models
{
    public partial class Address
    {

        public Address()
        {

            Customers = new HashSet<Customer>();

        }

        public int AddressId { get; set; }

        [Required(ErrorMessage = "An address is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(100, ErrorMessage = "A valid address up to 100 characters")]
        public string AddressName { get; set; } = null!;

        [Required(ErrorMessage = "A zip code is required")]
        [Column(TypeName = "char")]
        [StringLength(8, ErrorMessage = "A valid 8 characters zip code")]
        public string ZipCode { get; set; } = null!;

        [Required(ErrorMessage = "A city is required")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50, ErrorMessage = "A valid city up to 50 characters")]
        public string City { get; set; } = null!;

        #region Navigation Properties
        public virtual ICollection<Customer> Customers { get; set; }

        #endregion

    }
}
