using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class CartRepository : ICartRepository
    {
        private TechStoreDbContext context;

        public CartRepository(TechStoreDbContext context)
        {
            this.context = context;
        }

        public async Task CreateCartAsync(Cart cart)
        {
            await context.AddAsync(cart);
            await context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartItemByUserIDProductIDAsync(string userID, int productID)
        {
            return await context.Cart.FirstAsync(p => p.UserID == userID && p.ProductID == productID && !p.IsOrdered);                
        }

        public async Task<List<Cart>> GetAllCartItemsByOrderIDAsync(string orderID)
        {
            return await context.Cart.Where(p => p.OrderID == orderID).ToListAsync();
        }

        public async Task<List<Cart>> GetAllCartItemsByUserIDAsync(string userID)
        {
            return await context.Cart.Where(p => p.UserID == userID && !p.IsOrdered).ToListAsync();
        }

        public async Task<List<Cart>> GetAllCartsByProductIDAsync(int productID)
        {
            return await context.Cart.Where(p => p.ProductID == productID && !p.IsOrdered).ToListAsync();
        }

        public async Task<int> GetCartItemsCountByUserIDAsync(string userID)
        {
            return await context.Cart.Where(p => p.UserID == userID && !p.IsOrdered).SumAsync(p => p.Quantity);
        }

        public async Task<bool> DoesCartExistsAsync(string userID, int productID)
        {
            return await context.Cart.AnyAsync(p => p.UserID == userID && p.ProductID == productID && !p.IsOrdered);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        public async Task UpdateMultipleCartsAsync(List<Cart> carts)
        {
            context.UpdateRange(carts);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(Cart cart)
        {
            context.Remove(cart);
            await context.SaveChangesAsync();
        }

        public async Task DeleteMultipleCartsAsync(List<Cart> carts)
        {
            context.RemoveRange(carts);
            await context.SaveChangesAsync();
        }
    }
}
