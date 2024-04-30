using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class CartService
    {
        private TechStoreDbContext context;

        public CartService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void AddItemToCart(string userID, int productID)
        {
            Cart cart = new Cart(userID, productID, 1);
            if (context.Cart.Any(cart => cart.UserID == userID && cart.ProductID == productID && cart.PaymentID == 0))
            {
                Cart existingCart = GetCartByUserIDProductID(userID, productID);
                existingCart.Quantity++;
                context.Update(existingCart);
                context.SaveChanges();
            }
            else
            {
                context.Add(cart);
                context.SaveChanges();
            }
        }

        public void RemoveItemFromCart(string userID, int productID)
        {
            Cart cart = context.Cart.First(p => p.UserID == userID && p.ProductID == productID && p.PaymentID == 0);

            if (cart.Quantity > 1)
            {
                cart.Quantity--;
                context.Update(cart);
                context.SaveChanges();
            }
            else
            {
                context.Remove(cart);
                context.SaveChanges();
            }
        }

        public void UpdateCartsByUserID(string userID, int paymentID)
        {
            var cartsToUpdate = context.Cart.Where(p => p.UserID == userID && p.PaymentID == 0);
            foreach (var cart in cartsToUpdate)
            {
                cart.PaymentID = paymentID;
            }
            context.SaveChanges();
        }

        public List<Cart> GetAllCartsByPaymentID(int paymentID)
        {
            List<Cart> carts = context.Cart.Where(p => p.PaymentID == paymentID).ToList();
            return carts;
        }

        public Cart GetCartByUserIDProductID(string userID, int productID)
        {
            Cart cart = context.Cart.First(p => p.UserID == userID && p.ProductID == productID);
            return cart;
        }

        public List<Cart> GetAllCartProductsByUserID(string userID)
        {
            List<Cart> carts = context.Cart.Where(p => p.UserID == userID && p.PaymentID == 0).ToList();
            return carts;
        }        
    }
}
