using Marketplace.Data;
using Marketplace.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services
{
    public class CartService
    {
        private readonly MarketplaceDbContext _dbContext;

        public CartService(MarketplaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CartItemModel> GetCartItemsByUserId(int userId)
        {
            var cartItems = _dbContext.CartItem
                .Where(item => item.Cart.UserId == userId)
                .Include(item => item.Product) // Bao gồm các thuộc tính của sản phẩm
                .ToList();

            return cartItems;
        }
        public List<CartModel> GetCartsByUserId(int userId)
        {
            return _dbContext.Cart
                .Where(cart => cart.UserId == userId)
                .ToList();
        }

        public void AddToCart(int userId, int productId, int quantity)
        {
            var cart = _dbContext.Cart.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new CartModel
                {
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };
                _dbContext.Cart.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItemModel
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            _dbContext.SaveChanges();
        }
    }
}