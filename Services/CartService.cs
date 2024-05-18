using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;
using TechStore.Services.Interfaces;

namespace TechStore.Services
{
    public class CartService : ICartService
    {
        private ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public void AddProductToCart(string userID, int productID)
        {
            Cart cart = new Cart(userID, productID, 1);
            bool doesCartExists = cartRepository.DoesCartExists(userID, productID);
            if (doesCartExists)
            {
                Cart existingCart = cartRepository.GetCartItemByUserIDProductID(userID, productID);
                existingCart.Quantity++;
                cartRepository.UpdateCart(existingCart);
            }
            else
            {
                cartRepository.CreateCart(cart);
            }
        }

        public void RemoveProductFromCart(string userID, int productID)
        {
            Cart cart = cartRepository.GetCartItemByUserIDProductID(userID, productID);
            if (cart.Quantity > 1)
            {
                cart.Quantity--;
                cartRepository.UpdateCart(cart);
            }
            else
            {
                cartRepository.DeleteCart(cart);
            }
        }

        public List<Cart> GetAllCartItemsByOrderID(string orderID)
        {
            List<Cart> carts = cartRepository.GetAllCartItemsByOrderID(orderID);
            return carts;
        }

        public List<Cart> GetAllCartItemsByUserID(string userID)
        {
            List<Cart> carts = cartRepository.GetAllCartItemsByUserID(userID);
            return carts;
        }

        public List<Cart> GetAllCartItemsByTempData(List<int> productIDs, string userID)
        {
            List<Cart> carts = new List<Cart>();
            foreach (int productID in productIDs)
            {
                var existingCart = carts.FirstOrDefault(p => p.ProductID == productID);
                if (existingCart != null)
                {
                    existingCart.Quantity++;
                }
                else
                {
                    Cart newCart = new Cart(userID, productID, 1);
                    carts.Add(newCart);
                }
            }
            return carts;
        }

        public int GetCartItemsCountByUserID(string userID)
        {
            int cartItemsCount = cartRepository.GetCartItemsCountByUserID(userID);
            return cartItemsCount;
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

        public decimal ApplyPromocodeToTotalPrice(decimal totalPrice, decimal discount)
        {
            totalPrice -= Math.Round(totalPrice * (discount / 100), 2);
            return totalPrice;
        }

        public void DeleteCartsWithDeletedProduct(int productID)
        {
            List<Cart> carts = cartRepository.GetAllCartsByProductID(productID);
            cartRepository.DeleteMultipleCarts(carts);
        }

        public void UpdateCartItemsByUserID(string userID, string orderID)
        {
            List<Cart> carts = cartRepository.GetAllCartItemsByUserID(userID);
            foreach (var cart in carts)
            {
                cart.OrderID = orderID;
                cart.IsOrdered = true;
            }

            cartRepository.UpdateMultipleCarts(carts);
        }
    }
}
