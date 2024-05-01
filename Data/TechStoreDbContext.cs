using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TechStore.Data.Entities;

namespace TechStore.Data
{
    public class TechStoreDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Promocode> Promocode { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<ApplicationRole> Role { get; set; }
        public DbSet<ApplicationUser> User { get; set; }

        public TechStoreDbContext(DbContextOptions<TechStoreDbContext> options)
            : base(options)
        {
        }
    }
}
