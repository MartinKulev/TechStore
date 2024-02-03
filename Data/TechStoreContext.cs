using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TechStore.Data.Entities;

namespace TechStore.Data
{
    public class TechStoreContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductReview> ProductReview { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }

        public TechStoreContext(DbContextOptions<TechStoreContext> options) //Add-Migration Tech //Update-Database
            : base(options)
        { 
        }
    }
}
