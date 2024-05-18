using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface ICartService
    {
        void AddProductToCart(string userID, int productID);

        void RemoveProductFromCart(string userID, int productID);

        void UpdateCartItemsByUserID(string userID, string orderID);

        List<Cart> GetAllCartItemsByOrderID(string orderID);

        List<Cart> GetAllCartItemsByUserID(string userID);

        List<Cart> GetAllCartItemsByTempData(List<int> productIDs, string userID);

        int GetCartItemsCountByUserID(string userID);

        decimal CalculateCartTotalPrice(List<Cart> carts, List<Product> products);
        decimal ApplyPromocodeToTotalPrice(decimal totalPrice, decimal discount);

        void DeleteCartsWithDeletedProduct(int productID);
    }
}
