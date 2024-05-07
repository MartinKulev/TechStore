using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface ICartRepository
    {
        void CreateCart(Cart cart);

        Cart GetCartItemByUserIDProductID(string userID, int productID);

        List<Cart> GetAllCartItemsByOrderID(string orderID);

        List<Cart> GetAllCartItemsByUserID(string userID);

        List<Cart> GetAllCartsByProductID(int productID);

        int GetCartItemsCountByUserID(string userID);

        bool DoesCartExists(string userID, int productID);

        void UpdateCart(Cart cart);

        void UpdateMultipleCarts(List<Cart> carts);

        void DeleteCart(Cart cart);

        void DeleteMultipleCarts(List<Cart> carts);
    }
}
