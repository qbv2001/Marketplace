using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Models;
using Marketplace.Data;

namespace Marketplace.Controllers;

public class RegisterController : Controller
{
    private readonly string view = "~/Views/Auth/Register/Index.cshtml";
    private readonly ILogger<RegisterController> _logger;
    private readonly MarketplaceDbContext _dbContext;

    public RegisterController(ILogger<RegisterController> logger, MarketplaceDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        return View(view);
    }

    [HttpPost]
    public IActionResult Register(string fullName, string email, string phoneNumber, string password, string confirmPassword)
    {
        // Kiểm tra các thông tin đăng ký hợp lệ
        if (!IsValidRegistrationData(fullName, email, phoneNumber, password, confirmPassword))
        {
            ModelState.AddModelError(string.Empty, "Thông tin đăng ký không hợp lệ.");
            return View();
        }

         // Kiểm tra xem email đã tồn tại trong cơ sở dữ liệu chưa
        var existingUser = _dbContext.User.FirstOrDefault(u => u.Email == email);
        if (existingUser != null)
        {
            ModelState.AddModelError(string.Empty, "Email đã tồn tại.");
            return View();
        }

        // Kiểm tra xem số điện thoại đã tồn tại trong cơ sở dữ liệu chưa
        var existingUserPhone = _dbContext.User.FirstOrDefault(u => u.Email == email);
        if (existingUserPhone != null)
        {
            ModelState.AddModelError(string.Empty, "Số điện thoại đã tồn tại.");
            return View();
        }

        // Tạo tài khoản mới
        var newUser = new UserModel
        {
            Username = email, // Trong trường hợp này, sử dụng email làm tên đăng nhập
            FullName = fullName,
            Email = email,
            PhoneNumber = phoneNumber,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu (bạn nên sử dụng hashing và salt)
        newUser.Password = HashPassword(password);

        // Lưu thông tin đăng ký vào cơ sở dữ liệu
        _dbContext.User.Add(newUser);
        _dbContext.SaveChanges();

        // Chuyển hướng đến trang đăng nhập hoặc trang chủ sau khi đăng ký thành công
        return RedirectToAction("Login", "Account");
    }

    private bool IsValidRegistrationData(string fullName, string email, string phoneNumber, string password, string confirmPassword)
    {
        // Kiểm tra tính hợp lệ của thông tin đăng ký
        if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber)
            || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            return false;
        }

        // Kiểm tra định dạng email hợp lệ
        if (!IsValidEmail(email))
        {
            return false;
        }

        // Kiểm tra số điện thoại có đúng định dạng hợp lệ (nếu cần)

        // Kiểm tra mật khẩu có khớp với xác nhận mật khẩu
        if (password != confirmPassword)
        {
            return false;
        }

        // Kiểm tra các yêu cầu khác về tính hợp lệ của thông tin đăng ký (nếu cần)

        return true;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
