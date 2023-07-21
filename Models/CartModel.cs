using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class CartModel
    {
        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public UserModel User { get; set; }
        public List<CartItemModel> CartItems { get; set; }
    }
}
