using Marketplace.Data;
using Marketplace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Controllers
{
    public class DetailProductController : Controller
    {
        private readonly MarketplaceDbContext _dbContext;

        public DetailProductController(MarketplaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int spid)
        {
            var productDetail = _dbContext.Product.Include(p => p.Category).Include(p => p.Images).FirstOrDefault(p => p.ProductId == spid);
            ViewBag.productDetail = productDetail;
            var Reviews = _dbContext.Review.Include(p => p.User).ToList().Where(p => p.ProductId == spid);
            ViewBag.Reviews = Reviews;
            ViewBag.TotalReviews = Reviews.Count();

            List<int> TotalStarReviews = new List<int>{0,0,0,0,0};
            TotalStarReviews[0] = Reviews.Count(p => p.Rating == 1);
            TotalStarReviews[1] = Reviews.Count(p => p.Rating == 2);
            TotalStarReviews[2] = Reviews.Count(p => p.Rating == 3);
            TotalStarReviews[3] = Reviews.Count(p => p.Rating == 4);
            TotalStarReviews[4] = Reviews.Count(p => p.Rating == 5);
            ViewBag.TotalStarReviews = TotalStarReviews;

            float TotalStar = TotalStarReviews[0]+TotalStarReviews[1]*2+TotalStarReviews[2]*3+TotalStarReviews[3]*4+TotalStarReviews[4]*5;
            ViewBag.StarPercent = TotalStar / ViewBag.TotalReviews;
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                // Lưu thông tin sản phẩm vào cơ sở dữ liệu
                _dbContext.Product.Add(product);
                _dbContext.SaveChanges();
                
                // Điều hướng đến trang danh sách sản phẩm hoặc trang chi tiết sản phẩm đã đăng
                return RedirectToAction("Index", "Home");
            }
            
            return View(product);
        }


        // Thêm các action để xem danh sách đồ nội thất, thêm, sửa, xóa đồ nội thất, v.v.
    }
}
