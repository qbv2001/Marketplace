using System.Security.Cryptography;
using System.Text;
using Marketplace.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data
{
    public class MarketplaceDbContext : DbContext
    {
        public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> User { get; set; }

        public DbSet<ProductModel> Product { get; set; }

        public DbSet<CategoryModel> Category { get; set; }

        public DbSet<TransactionModel> Transaction { get; set; }

        public DbSet<ReviewModel> Review { get; set; }

        public DbSet<CartModel> Cart { get; set; }

        public DbSet<CartItemModel> CartItem { get; set; }
        public DbSet<AddressModel> Address { get; set; }
        public DbSet<ImageModel> Image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Định nghĩa mối quan hệ giữa TransactionModel và UserModel

            modelBuilder.Entity<TransactionModel>()
                .HasOne(t => t.Seller)         // Một Transaction có một Seller
                .WithMany(u => u.SoldItems)    // Một Seller có nhiều giao dịch (người bán)
                .HasForeignKey(t => t.SellerId); // Khóa ngoại là SellerId

            modelBuilder.Entity<TransactionModel>()
                .HasOne(t => t.Buyer)          // Một Transaction có một Buyer
                .WithMany(u => u.PurchasedItems) // Một Buyer có nhiều giao dịch (người mua)
                .HasForeignKey(t => t.BuyerId); // Khóa ngoại là BuyerId

            // Các định nghĩa mối quan hệ khác nếu có

            // Thêm dữ liệu mẫu cho bảng Categories
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { CategoryId = 1, Name = "Thời trang và quần áo" },
                new CategoryModel { CategoryId = 2, Name = "Xe hơi và phương tiện" },
                new CategoryModel { CategoryId = 3, Name = "Nông nghiệp và nông nghiệp" },
                new CategoryModel { CategoryId = 4, Name = "Điện tử và công nghệ" },
                new CategoryModel { CategoryId = 5, Name = "Đóng gói và in ấn" },
                new CategoryModel { CategoryId = 6, Name = "Nhà cửa và nhà bếp" },
                new CategoryModel { CategoryId = 7, Name = "Sản phẩm kỹ thuật số" }
            );

            // Thêm dữ liệu mẫu cho bảng Users
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel { UserId = 1, Username = "huutinh", Password = HashPassword("123456"), Email = "huutinh@example.com", FullName = "Nguyễn Hữu Tình", PhoneNumber = "0835133683",ImageUrl = "/images/avatars/avatar1.jpg", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new UserModel { UserId = 2, Username = "kieudiem", Password = HashPassword("123456"), Email = "kieudiem@example.com", FullName = "Kiều Diễm", PhoneNumber = "0835133683", ImageUrl = "/images/avatars/avatar2.jpg", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new UserModel { UserId = 3, Username = "maihoa", Password = HashPassword("123456"), Email = "maihoa@example.com", FullName = "Mai Hoa", PhoneNumber = "0835133683", ImageUrl = "/images/avatars/avatar3.jpg", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

            // Thêm dữ liệu mẫu cho bảng Products
            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel { ProductId = 1, SellerId = 1, CategoryId = 1, Name = "Sản phẩm 1", Description = "Mô tả sản phẩm 1", Price = 100, ImageUrl = "/images/items/1.jpg", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductModel { ProductId = 2, SellerId = 2, CategoryId = 2, Name = "Sản phẩm 2", Description = "Mô tả sản phẩm 2", Price = 200, ImageUrl = "/images/items/2.jpg", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ProductModel { ProductId = 3, SellerId = 2, CategoryId = 2, Name = "Sản phẩm 3", Description = "Mô tả sản phẩm 3", Price = 300, ImageUrl = "/images/items/3.jpg", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }

    );

            // Thêm dữ liệu mẫu cho bảng Transactions
            modelBuilder.Entity<TransactionModel>().HasData(
                new TransactionModel { TransactionId = 1, ProductId = 1, SellerId = 1, BuyerId = 2, Price = 100, Quantity = 1 },
                new TransactionModel { TransactionId = 2, ProductId = 2, SellerId = 2, BuyerId = 1, Price = 200, Quantity = 2 }
                // Thêm các giao dịch khác nếu cần
            );

            // Thêm dữ liệu mẫu cho bảng Reviews
            modelBuilder.Entity<ReviewModel>().HasData(
                new ReviewModel { ReviewId = 1, ProductId = 1, UserId = 2, Rating = 5, Comment = "Sản phẩm tuyệt vời!", CreatedAt = DateTime.Now },
                new ReviewModel { ReviewId = 2, ProductId = 2, UserId = 1, Rating = 4, Comment = "Sản phẩm tốt, nhưng có thể cải thiện hơn", CreatedAt = DateTime.Now }
                // Thêm các đánh giá khác nếu cần
            );

            modelBuilder.Entity<AddressModel>().HasData(
                new AddressModel { AddressId = 1, UserId = 1, AddressLine1 = "123 Đường Chính", AddressLine2 = "123 Đường Chính", State = "on", City = "Hà Nội", Country = "Việt Nam" },
                new AddressModel { AddressId = 2, UserId = 2, AddressLine1 = "456 Đường Láng", AddressLine2 = "123 Đường Láng", State = "on", City = "TP.HCM", Country = "Việt Nam" }
            );

            // Thêm dữ liệu mẫu cho bảng Images
            modelBuilder.Entity<ImageModel>().HasData(
                new ImageModel { ImageId = 1, ProductId = 1, ImageUrl = "/images/items/1.jpg" },
                new ImageModel { ImageId = 2, ProductId = 2, ImageUrl = "/images/items/2.jpg" }
            );

            // Tạo dữ liệu mẫu cho bảng Cart
            modelBuilder.Entity<CartModel>().HasData(
                new CartModel { CartId = 1, CreatedAt = DateTime.Now, UserId = 1 },
                new CartModel { CartId = 2, CreatedAt = DateTime.Now, UserId = 2 }
            );

            // Tạo dữ liệu mẫu cho bảng CartItem
            modelBuilder.Entity<CartItemModel>().HasData(
                new CartItemModel { CartItemId = 1, CartId = 1, ProductId = 1, Quantity = 2 },
                new CartItemModel { CartItemId = 2, CartId = 1, ProductId = 2, Quantity = 1},
                new CartItemModel { CartItemId = 3, CartId = 2, ProductId = 3, Quantity = 3}
            );

            base.OnModelCreating(modelBuilder);
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Chuyển đổi mật khẩu thành mảng byte
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Mã hóa mật khẩu bằng SHA256
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                // Chuyển đổi mảng byte thành chuỗi hexa
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        
    }
}
