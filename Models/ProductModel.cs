using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CategoryModel Category { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
