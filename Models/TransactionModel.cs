using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public DateTime TransactionDate { get; set; }

        public UserModel Seller { get; set; }
        public UserModel Buyer { get; set; }
        public ProductModel Product { get; set; }


    }
}
