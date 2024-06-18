using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<Cart> GetCartItemByUserIDProductIDAsync(string userID, int productID);

        Task<List<Cart>> GetAllCartItemsByOrderIDAsync(string orderID);

        Task<List<Cart>> GetAllCartItemsByUserIDAsync(string userID);

        Task<List<Cart>> GetAllCartsByProductIDAsync(int productID);

        Task<int> GetCartItemsCountByUserIDAsync(string userID);

        Task<bool> DoesCartExistsAsync(string userID, int productID);
    }
}
