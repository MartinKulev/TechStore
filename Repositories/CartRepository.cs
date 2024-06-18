using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        private TechStoreDbContext context;

        public CartRepository(TechStoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Cart> GetCartItemByUserIDProductIDAsync(string userID, int productID)
        {
            return await context.Carts.FirstAsync(p => p.UserID == userID && p.ProductID == productID && !p.IsOrdered);                
        }

        public async Task<List<Cart>> GetAllCartItemsByOrderIDAsync(string orderID)
        {
            return await context.Carts.Where(p => p.OrderID == orderID).ToListAsync();
        }

        public async Task<List<Cart>> GetAllCartItemsByUserIDAsync(string userID)
        {
            return await context.Carts.Where(p => p.UserID == userID && !p.IsOrdered).ToListAsync();
        }

        public async Task<List<Cart>> GetAllCartsByProductIDAsync(int productID)
        {
            return await context.Carts.Where(p => p.ProductID == productID && !p.IsOrdered).ToListAsync();
        }

        public async Task<int> GetCartItemsCountByUserIDAsync(string userID)
        {
            return await context.Carts.Where(p => p.UserID == userID && !p.IsOrdered).SumAsync(p => p.Quantity);
        }

        public async Task<bool> DoesCartExistsAsync(string userID, int productID)
        {
            return await context.Carts.AnyAsync(p => p.UserID == userID && p.ProductID == productID && !p.IsOrdered);
        }
    }
}
