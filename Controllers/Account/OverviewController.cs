using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Models;
using Marketplace.Data;
using Microsoft.EntityFrameworkCore;
using Marketplace.Services;

namespace Marketplace.Controllers;

public class OverviewController : Controller
{
    private readonly ILogger<OverviewController> _logger;
    private readonly MarketplaceDbContext _dbContext;

    public OverviewController(ILogger<OverviewController> logger, MarketplaceDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;

    }

    public IActionResult Index()
    {
         // Truy xuất thông tin đăng nhập từ session
        int? userId = HttpContext.Session.GetInt32("UserId");
        string username = HttpContext.Session.GetString("Username");

        // Kiểm tra xem người dùng đã đăng nhập chưa
        if (userId.HasValue)
        {
            // Xử lý và trả về danh sách giỏ hàng
            return View("~/Views/Account/Overview.cshtml");        }
        else
        {
            // Người dùng chưa đăng nhập
            return RedirectToAction("Index","login");
        }
    }

    [HttpPost]

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
