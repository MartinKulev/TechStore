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

        public string CreateOrder(Order order)
        {
            context.Add(order);
            context.SaveChanges();
            context.Entry(order).Reload();
            string orderID = order.OrderID;
            return orderID;
        }

        public List<Order> GetAllOrdersByUserID(string usedID)
        {
            List<Order> orders = context.Order.Where(p => p.UserID == usedID).OrderByDescending(p => p.OrderTime).ToList();
            return orders;
        }

        public Order GetOrderByID(string orderID)
        {
            Order order = context.Order.First(p => p.OrderID == orderID);
            return order;
        }
    }
}
