using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(string userID, string name, string cardNumber, string expiryDate, int cvvNum, string adress, decimal totalPrice, decimal oldTotalPrice);

        Task<List<Order>> GetAllOrdersByUserIDAsync(string usedID);

        Task<Order> GetOrderByIDAsync(string orderID);
    }
}
