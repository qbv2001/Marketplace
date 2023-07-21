using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public partial class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public List<ProductModel> CartItems { get; set; }
    }
}
