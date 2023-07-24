using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Models;
using Marketplace.Data;

namespace Marketplace.Controllers;

public class LoginController : Controller
{
    private readonly string view = "~/Views/Auth/Login/Index.cshtml";
    private readonly ILogger<LoginController> _logger;
    private readonly MarketplaceDbContext _dbContext;

    public LoginController(ILogger<LoginController> logger, MarketplaceDbContext dbContext)
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
            // Đăng nhập thành công
            return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ hoặc trang yêu cầu đăng nhập trước đó
        }
        else
        {
            // Người dùng chưa đăng nhập
            return View(view);
        }
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Kiểm tra thông tin đăng nhập
        var user = _dbContext.User.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            // Mã hóa mật khẩu nhập vào từ người dùng
            var hashedPassword = RegisterController.HashPassword(password);

            // So sánh với mật khẩu đã lưu trong cơ sở dữ liệu
            if (user.Password == hashedPassword)
            {
                // Đăng nhập thành công, lưu thông tin đăng nhập vào session
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }
        }
        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
        return RedirectToAction("Index", "Login");
    }

    public IActionResult Logout()
    {
        // Xóa session hoặc token hiện tại của người dùng
        HttpContext.Session.Clear(); // Đối với session

        // Chuyển hướng về trang chủ hoặc trang đăng nhập
        return RedirectToAction("Index", "Home"); //
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
