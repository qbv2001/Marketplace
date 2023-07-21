using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        
        public List<ProductModel> Products { get; set; }
    }
}
