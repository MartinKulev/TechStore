using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class OrderService
    {
        private TechStoreDbContext context;

        public OrderService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public string CreateOrder(Order order)
        {
            context.Order.Add(order);
            context.SaveChanges();

            context.Entry(order).Reload();
            string generatedKey = order.OrderID.ToString();
            return generatedKey;
        }

        public List<Order> GetAllOrdersByUserID(string usedID)
        {
            List<Order> orders = context.Order.Where(p => p.UserID == usedID).ToList();
            return orders;
        }

        public Order GetOrderByID(string orderID)
        {
            Order order = context.Order.First(p => p.OrderID == orderID);
            return order;
        }
    }
}
