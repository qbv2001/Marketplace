using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class ReviewModel
    {
        [Key]
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProductModel Product { get; set; }
        public UserModel User { get; set; }
    }
}
