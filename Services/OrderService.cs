using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;
using TechStore.Services.Interfaces;

namespace TechStore.Services
{
    public class OrderService : IOrderService
    {
        private ICartService cartService;
        private IProductService productService;
        private IOrderRepository orderRepository;

        public OrderService(ICartService cartService, IProductService productService, IOrderRepository orderRepository)
        {
            this.cartService = cartService;
            this.productService = productService;
            this.orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(string userID, string name, string cardNumber, string expiryDate, int cvvNum, string address, decimal totalPrice, decimal oldTotalPrice)
        {
            List<Cart> cartItems = await cartService.GetAllCartItemsByUserIDAsync(userID);
            DateTime dateTimeNow = DateTime.UtcNow + TimeSpan.FromHours(3);
            List<Product> productsInCart = await productService.GetAllProductsInCartAsync(cartItems);

            Order order = new Order(name, cardNumber, expiryDate, cvvNum, address, userID, totalPrice, oldTotalPrice);
            string orderID = await orderRepository.CreateOrderAsync(order);
            await cartService.UpdateCartItemsByUserIDAsync(userID, orderID);
        }

        public async Task<List<Order>> GetAllOrdersByUserIDAsync(string userID)
        {
            return await orderRepository.GetAllOrdersByUserIDAsync(userID);
        }

        public async Task<Order> GetOrderByIDAsync(string orderID)
        {
            return await orderRepository.GetOrderByIDAsync(orderID);
        }
    }
}
