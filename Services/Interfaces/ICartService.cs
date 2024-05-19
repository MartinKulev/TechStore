using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface ICartService
    {
        Task AddProductToCartAsync(string userID, int productID);

        Task RemoveProductFromCartAsync(string userID, int productID);

        Task UpdateCartItemsByUserIDAsync(string userID, string orderID);

        Task<List<Cart>> GetAllCartItemsByOrderIDAsync(string orderID);

        Task<List<Cart>> GetAllCartItemsByUserIDAsync(string userID);

        List<Cart> GetAllCartItemsByTempData(List<int> productIDs, string userID);

        Task<int> GetCartItemsCountByUserIDAsync(string userID);

        decimal CalculateCartTotalPrice(List<Cart> carts, List<Product> products);

        decimal ApplyPromocodeToTotalPrice(decimal totalPrice, decimal discount);

        Task DeleteCartsWithDeletedProductAsync(int productID);
    }
}
