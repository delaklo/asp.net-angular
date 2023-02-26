using Microsoft.EntityFrameworkCore;
using shop.API.Models;

namespace shop.API.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
