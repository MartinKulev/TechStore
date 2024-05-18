using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(string userID, string name, string cardNumber, string expiryDate, int cvvNum, string adress, decimal totalPrice, decimal oldTotalPrice);

        List<Order> GetAllOrdersByUserID(string usedID);

        Order GetOrderByID(string orderID);
    }
}
