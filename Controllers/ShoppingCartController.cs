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
        var cartList = _cartService.GetCartItemsByUserId(1);
        ViewBag.cartList = cartList;

        // Xử lý và trả về danh sách giỏ hàng
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
