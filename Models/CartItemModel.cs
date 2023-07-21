using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class CartItemModel
    {
        [Key]
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public CartModel Cart { get; set; }
        public ProductModel Product { get; set; }
        
    }
}
