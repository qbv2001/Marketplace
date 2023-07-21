using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class ImageModel
    {
        [Key]
        public int ImageId  { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }

        public ProductModel Product { get; set; } 
        
    }
}
