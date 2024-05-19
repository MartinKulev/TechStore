using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task CreateCartAsync(Cart cart);

        Task<Cart> GetCartItemByUserIDProductIDAsync(string userID, int productID);

        Task<List<Cart>> GetAllCartItemsByOrderIDAsync(string orderID);

        Task<List<Cart>> GetAllCartItemsByUserIDAsync(string userID);

        Task<List<Cart>> GetAllCartsByProductIDAsync(int productID);

        Task<int> GetCartItemsCountByUserIDAsync(string userID);

        Task<bool> DoesCartExistsAsync(string userID, int productID);

        Task UpdateCartAsync(Cart cart);

        Task UpdateMultipleCartsAsync(List<Cart> carts);

        Task DeleteCartAsync(Cart cart);

        Task DeleteMultipleCartsAsync(List<Cart> carts);
    }
}
