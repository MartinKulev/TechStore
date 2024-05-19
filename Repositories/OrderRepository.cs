using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private TechStoreDbContext context;

        public OrderRepository(TechStoreDbContext context)
        {
            this.context = context;
        }

        public async Task<string> CreateOrderAsync(Order order)
        {
            await context.AddAsync(order);
            await context.SaveChangesAsync();
            await context.Entry(order).ReloadAsync();
            return order.OrderID;
        }

        public async Task<List<Order>> GetAllOrdersByUserIDAsync(string userID)
        {
            return await context.Orders.Where(p => p.UserID == userID).OrderByDescending(p => p.OrderTime).ToListAsync();
        }

        public async Task<Order> GetOrderByIDAsync(string orderID)
        {
            return await context.Orders.FirstAsync(p => p.OrderID == orderID);
        }
    }
}
