using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class AddressModel
    {
        [Key]
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public UserModel User { get; set; }
    }
}
