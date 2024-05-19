using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<string> CreateOrderAsync(Order order);

        Task<List<Order>> GetAllOrdersByUserIDAsync(string usedID);

        Task<Order> GetOrderByIDAsync(string orderID);
    }
}
