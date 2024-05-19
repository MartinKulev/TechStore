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

        public async Task AddProductToCartAsync(string userID, int productID)
        {
            Cart cart = new Cart(userID, productID, 1);
            bool doesCartExists = await cartRepository.DoesCartExistsAsync(userID, productID);
            if (doesCartExists)
            {
                Cart existingCart = await cartRepository.GetCartItemByUserIDProductIDAsync(userID, productID);
                existingCart.Quantity++;
                await cartRepository.UpdateCartAsync(existingCart);
            }
            else
            {
                await cartRepository.CreateCartAsync(cart);
            }
        }

        public async Task RemoveProductFromCartAsync(string userID, int productID)
        {
            Cart cart = await cartRepository.GetCartItemByUserIDProductIDAsync(userID, productID);
            if (cart.Quantity > 1)
            {
                cart.Quantity--;
                await cartRepository.UpdateCartAsync(cart);
            }
            else
            {
                await cartRepository.DeleteCartAsync(cart);
            }
        }

        public async Task<List<Cart>> GetAllCartItemsByOrderIDAsync(string orderID)
        {
            return await cartRepository.GetAllCartItemsByOrderIDAsync(orderID);
        }

        public async Task<List<Cart>> GetAllCartItemsByUserIDAsync(string userID)
        {
            return await cartRepository.GetAllCartItemsByUserIDAsync(userID);
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

        public async Task<int> GetCartItemsCountByUserIDAsync(string userID)
        {
            return await cartRepository.GetCartItemsCountByUserIDAsync(userID);
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

        public async Task DeleteCartsWithDeletedProductAsync(int productID)
        {
            List<Cart> carts = await cartRepository.GetAllCartsByProductIDAsync(productID);
            await cartRepository.DeleteMultipleCartsAsync(carts);
        }

        public async Task UpdateCartItemsByUserIDAsync(string userID, string orderID)
        {
            List<Cart> carts = await cartRepository.GetAllCartItemsByUserIDAsync(userID);
            foreach (var cart in carts)
            {
                cart.OrderID = orderID;
                cart.IsOrdered = true;
            }

            await cartRepository.UpdateMultipleCartsAsync(carts);
        }
    }
}
