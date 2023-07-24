using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Models;
using Marketplace.Data;
using Microsoft.EntityFrameworkCore;
using Marketplace.Services;

namespace Marketplace.Controllers;

public class ShoppingCartController : Controller
{
    private readonly ILogger<ShoppingCartController> _logger;
    private readonly CartService _cartService;
    private readonly MarketplaceDbContext _dbContext;

    public ShoppingCartController(ILogger<ShoppingCartController> logger, MarketplaceDbContext dbContext, CartService cartService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _cartService = cartService;

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
            var cartList = _cartService.GetCartItemsByUserId((int)userId);
            ViewBag.cartList = cartList;

            // Xử lý và trả về danh sách giỏ hàng
            return View();        }
        else
        {
            // Người dùng chưa đăng nhập
            return RedirectToAction("Index","login");
        }
    }

    [HttpPost]
    public IActionResult AddToCart(int productId,int quantity)
    {
        try
        {
            // Lấy sản phẩm từ cơ sở dữ liệu dựa trên productId
            var product = _dbContext.Product.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound(); // Nếu không tìm thấy sản phẩm, trả về lỗi 404
            }

            // Lấy sản phẩm từ cơ sở dữ liệu dựa trên userid
             // Truy xuất thông tin đăng nhập từ session
            int? userId = HttpContext.Session.GetInt32("UserId");
            string username = HttpContext.Session.GetString("Username");

            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (userId.HasValue)
            {
                var cart = _dbContext.Cart.FirstOrDefault(p => p.UserId == userId);
                if (cart == null)
                {
                     // Tạo một giỏ hàng mới
                    var cartdata = new CartModel
                    {   
                        UserId = (int)userId,
                        CreatedAt = DateTime.Now,
                    };
                    _dbContext.Cart.Add(cartdata);
                    _dbContext.SaveChanges();
                    cart = _dbContext.Cart.FirstOrDefault(p => p.UserId == userId);
                }

                var cartitemcheck = _dbContext.CartItem.FirstOrDefault(p => p.ProductId == productId);
                if (cartitemcheck != null)
                {
                     // Nếu sản phẩm đã tồn tại trong giỏ hàng, thực hiện cập nhật thông tin của sản phẩm
                    cartitemcheck.Quantity+=quantity; // Ví dụ: Tăng số lượng sản phẩm lên 1

                    // Cập nhật thông tin của sản phẩm trong cơ sở dữ liệu
                    _dbContext.CartItem.Update(cartitemcheck);
                    _dbContext.SaveChanges();
                }else{
                    // Tạo một giỏ hàng mới
                    var cartItem = new CartItemModel
                    {   
                        CartId = cart.CartId,
                        ProductId = productId,
                        Quantity = quantity // Số lượng sản phẩm mặc định là 1, bạn có thể thay đổi nếu cần
                    };

                    // Thêm giỏ hàng vào cơ sở dữ liệu
                    _dbContext.CartItem.Add(cartItem);
                    _dbContext.SaveChanges();
                }
            }
            else
            {
                return NotFound();
            }
            

            return Json(product); // Trả về phản hồi 200 OK nếu thêm vào giỏ hàng thành công
        }
        catch (Exception ex)
        {
            // Xử lý các lỗi khác (nếu có)
            return BadRequest(); // Trả về lỗi 400 Bad Request nếu có lỗi xảy ra
        }
    }
    
    [HttpPost]
    public IActionResult DeleteCartItem(int productId)
    {
        // Lấy sản phẩm từ cơ sở dữ liệu dựa trên productId
        var cartitem = _dbContext.CartItem.FirstOrDefault(p => p.ProductId == productId);

        if (cartitem != null)
        {
            // Xóa sản phẩm khỏi cơ sở dữ liệu
            _dbContext.CartItem.Remove(cartitem);

            // Lưu thay đổi vào cơ sở dữ liệu
            _dbContext.SaveChanges();
        }

        return Ok(); // Trả về phản hồi 200 OK nếu xóa thành công
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
