using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TechStore.Data.Entities;

namespace TechStore.Data
{
    public class TechStoreContext : DbContext
    {
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Category> Category { get; set; }        
        public DbSet<Order> Order { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }

        public TechStoreContext(DbContextOptions<TechStoreContext> options) 
            : base(options)
        { 
        }
    }
}
