using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.MauiClient.Models;

namespace ProductManagementSystem.MauiClient.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductItem> Products { get; set; }
    }
}
