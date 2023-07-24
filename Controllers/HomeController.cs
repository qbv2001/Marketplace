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
        var productList = GetProductsForPage(1);
        var categoryList = _dbContext.Category.ToList();

        ViewBag.productList = productList;
        ViewBag.categoryList = categoryList;
        return View();
    }

    // Action xử lý khi người dùng nhấn nút "Tìm kiếm"
    [HttpGet]
    public IActionResult Search(string keyword, int minValue=0, int maxValue=0, int page=1)
    {
        // Thực hiện truy vấn cơ sở dữ liệu để lấy danh sách các sản phẩm phù hợp với từ khóa tìm kiếm
        var products = GetProductsForPage(page, keyword,minValue,maxValue);

        ViewBag.products = products;
        ViewBag.Keyword = keyword;
        ViewBag.MinValue = minValue;
        ViewBag.MaxValue = maxValue;
        // Trả về view "search" và truyền danh sách sản phẩm phù hợp vào view để hiển thị kết quả
        return View("~/Views/Search/Index.cshtml");
    }

    public IActionResult LoadMoreProducts(int page)
    {
        // Xử lý phân trang và lấy danh sách sản phẩm cần hiển thị
        var products = GetProductsForPage(page);

        return Json(products);
    }
    
    public List<ProductModel> GetProductsForPage(int page, string keyword = "", int minValue = 0, int maxValue = 0)
    {
        int itemsPerPage = 1;
        // Số sản phẩm bắt đầu từ (page - 1) * itemsPerPage
        int skipProducts = (page - 1) * itemsPerPage;

        // Lấy danh sách sản phẩm từ database hoặc nguồn dữ liệu khác
        var products = new List<ProductModel>();

        if(maxValue == 0){
            products = _dbContext.Product.Where(p => p.Name.Contains(keyword) && p.Price >= minValue).ToList();
        }else{
            products = _dbContext.Product.Where(p => p.Name.Contains(keyword) && p.Price >= minValue && p.Price <= maxValue).ToList();
        }

        var productsForPage = products.Skip(skipProducts).Take(itemsPerPage).ToList();

         // Tính tổng số trang
        var totalItems = products.Count;
        var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

        // Truyền các thông tin cần thiết vào ViewBag hoặc ViewData để sử dụng trong View
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.producttotal = totalItems;
        return productsForPage;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
