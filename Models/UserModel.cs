using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<TransactionModel> SoldItems { get; set; }
        public List<TransactionModel> PurchasedItems { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }
}
