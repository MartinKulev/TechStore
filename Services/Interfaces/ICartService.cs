using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface ICartService
    {
        void AddProductToCart(string userID, int productID);

        void RemoveProductFromCart(string userID, int productID);

        void UpdateCartItemsByUserID(string userID, string orderID);

        List<Cart> GetAllCartItemsByOrderID(string orderID);

        Cart GetCartItemByUserIDProductID(string userID, int productID);

        List<Cart> GetAllCartItemsByUserID(string userID);

        List<Cart> GetAllCartItemsByTempData(List<int> productIDs, string userId);

        int GetCartItemsCountByUserID(string userID);

        decimal CalculateCartTotalPrice(List<Cart> carts, List<Product> products);
    }
}
