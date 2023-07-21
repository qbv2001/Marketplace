using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Marketplace.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Products = new HashSet<ProductModel>();
        }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
