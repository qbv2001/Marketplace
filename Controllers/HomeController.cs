using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Models;
using Marketplace.Data;

namespace Marketplace.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MarketplaceDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, MarketplaceDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var productList = GetProductsForPage(1, 1);
        var categoryList = _dbContext.Category.ToList();

        ViewBag.productList = productList;
        ViewBag.categoryList = categoryList;
        return View();
    }

    public IActionResult LoadMoreProducts(int page, int itemsPerPage)
    {
        // Xử lý phân trang và lấy danh sách sản phẩm cần hiển thị
        var products = GetProductsForPage(page, itemsPerPage);

        return Json(products);
    }
    
    public List<ProductModel> GetProductsForPage(int page, int itemsPerPage)
    {
        // Số sản phẩm bắt đầu từ (page - 1) * itemsPerPage
        int skipProducts = (page - 1) * itemsPerPage;

        // Lấy danh sách sản phẩm từ database hoặc nguồn dữ liệu khác
        // Trong ví dụ này, ta giả định có một danh sách sản phẩm đã được lấy từ database
        var productsForPage = _dbContext.Product.Skip(skipProducts).Take(itemsPerPage).ToList();
        return productsForPage;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
