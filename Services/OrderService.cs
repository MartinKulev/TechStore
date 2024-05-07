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

        public void CreateOrder(string userID, string name, string cardNumber, string expiryDate, int cvvNum, string adress)
        {
            List<Cart> cartItems = cartService.GetAllCartItemsByUserID(userID);
            DateTime dateTimeNow = DateTime.UtcNow + TimeSpan.FromHours(3);
            List<Product> productsInCart = productService.GetAllProductsInCart(cartItems);
            decimal totalPrice = cartService.CalculateCartTotalPrice(cartItems, productsInCart);

            Order order = new Order(name, cardNumber, expiryDate, cvvNum, adress, userID, totalPrice, dateTimeNow);
            string orderID = orderRepository.CreateOrder(order);
            cartService.UpdateCartItemsByUserID(userID, orderID);
        }

        public List<Order> GetAllOrdersByUserID(string usedID)
        {
            List<Order> orders = orderRepository.GetAllOrdersByUserID(usedID);
            return orders;
        }

        public Order GetOrderByID(string orderID)
        {
            Order order = orderRepository.GetOrderByID(orderID);
            return order;
        }
    }
}
