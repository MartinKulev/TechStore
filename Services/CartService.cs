using Microsoft.AspNetCore.Cors.Infrastructure;
using Mysqlx.Crud;
using System.Security.Claims;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Data.ViewModels;

namespace TechStore.Services
{
    public class CartService
    {
        private TechStoreDbContext context;

        public CartService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void AddProductToCart(string userID, int productID)
        {
            Cart cart = new Cart(userID, productID, 1);
            if (context.Cart.Any(cart => cart.UserID == userID && cart.ProductID == productID && cart.OrderID == 0))
            {
                Cart existingCart = GetCartItemByUserIDProductID(userID, productID);
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

        public void RemoveProductFromCart(string userID, int productID)
        {
            Cart cart = context.Cart.First(p => p.UserID == userID && p.ProductID == productID && p.OrderID == 0);

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

        public void UpdateCartItemsByUserID(string userID, int orderID)
        {
            var cartsToUpdate = context.Cart.Where(p => p.UserID == userID && p.OrderID == 0);
            foreach (var cart in cartsToUpdate)
            {
                cart.OrderID = orderID;
            }
            context.SaveChanges();
        }

        public List<Cart> GetAllCartItemsByOrderID(int orderID)
        {
            List<Cart> carts = context.Cart.Where(p => p.OrderID == orderID).ToList();
            return carts;
        }

        public Cart GetCartItemByUserIDProductID(string userID, int productID)
        {
            Cart cart = context.Cart.First(p => p.UserID == userID && p.ProductID == productID);
            return cart;
        }

        public List<Cart> GetAllCartItemsByUserID(string userID)
        {
            List<Cart> carts = context.Cart.Where(p => p.UserID == userID && p.OrderID == 0).ToList();
            return carts;
        }

        public List<Cart> GetAllCartItemsByTempData(List<int> productIDs, string userId)
        {
            List<Cart> carts = new List<Cart>();
            foreach (int productID in productIDs)
            {
                var existingCart = carts.FirstOrDefault(cart => cart.ProductID == productID);
                if (existingCart != null)
                {
                    existingCart.Quantity++;
                }
                else
                {
                    Cart newCart = new Cart(userId, productID, 1);
                    carts.Add(newCart);
                }
            }
            return carts;
        }

        public decimal CalculateCartTotalPrice(List<Cart> carts, List<Product> products)
        {
            decimal totalPrice = 0;
            foreach (var cart in carts)
            {
                Product product = products.First(p => p.ProductID == cart.ProductID);
                if (product.IsInPromotion)
                {
                    totalPrice += (product.NewPrice * cart.Quantity);
                }
                else
                {
                    totalPrice += (product.Price * cart.Quantity);
                }
            }
            return totalPrice;
        }
    }
}
