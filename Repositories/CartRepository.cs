using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class CartRepository : ICartRepository
    {
        private TechStoreDbContext context;

        public CartRepository(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void CreateCart(Cart cart)
        {
            context.Add(cart);
            context.SaveChanges();
        }
        public Cart GetCartItemByUserIDProductID(string userID, int productID)
        {
            Cart cart = context.Cart.First(p => p.UserID == userID && p.ProductID == productID && !p.IsOrdered);
            return cart;
        }


        public List<Cart> GetAllCartItemsByOrderID(string orderID)
        {
            List<Cart> carts = context.Cart.Where(p => p.OrderID == orderID).ToList();
            return carts;
        }

        public List<Cart> GetAllCartItemsByUserID(string userID)
        {
            List<Cart> carts = context.Cart.Where(p => p.UserID == userID && !p.IsOrdered).ToList();
            return carts;
        }

        public List<Cart> GetAllCartsByProductID(int productID)
        {
            List<Cart> carts = context.Cart.Where(p => p.ProductID == productID && !p.IsOrdered).ToList();
            return carts;
        }

        public int GetCartItemsCountByUserID(string userID)
        {
            int cartItemsCount = context.Cart.Where(p => p.UserID == userID && !p.IsOrdered).Sum(p => p.Quantity);
            return cartItemsCount;
        }

        public bool DoesCartExists(string userID, int productID)
        {
            bool doesCartExists = context.Cart.Any(p => p.UserID == userID && p.ProductID == productID && !p.IsOrdered);
            return doesCartExists;
        }

        public void UpdateCart(Cart cart)
        {
            context.Update(cart);
            context.SaveChanges();
        }

        public void UpdateMultipleCarts(List<Cart> carts)
        {
            context.UpdateRange(carts);
            context.SaveChanges();
        }

        public void DeleteCart(Cart cart)
        {
            context.Remove(cart);
            context.SaveChanges();
        }

        public void DeleteMultipleCarts(List<Cart> carts)
        {
            context.RemoveRange(carts);
            context.SaveChanges();
        }
    }
}
