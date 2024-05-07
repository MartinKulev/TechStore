using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        string CreateOrder(Order order);

        List<Order> GetAllOrdersByUserID(string usedID);

        Order GetOrderByID(string orderID);
    }
}
