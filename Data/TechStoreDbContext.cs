using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechStore.Data.Entities;

namespace TechStore.Data
{
    public class TechStoreDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Promocode> Promocodes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        public TechStoreDbContext(DbContextOptions<TechStoreDbContext> options)
            : base(options)
        {
        }
    }
}
