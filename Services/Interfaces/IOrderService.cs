using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(string userId, string name, string cardNumber, string expiryDate, int cvvNum, string adress);

        List<Order> GetAllOrdersByUserID(string usedID);

        Order GetOrderByID(string orderID);
    }
}
