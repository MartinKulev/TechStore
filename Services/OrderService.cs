using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class OrderService
    {
        private TechStoreDbContext context;
        private CartService cartService;
        private ProductService productService;

        public OrderService(TechStoreDbContext context, CartService cartService, ProductService productService)
        {
            this.context = context;
            this.cartService = cartService;
            this.productService = productService;
        }

        public void CreateOrder(string userId, string name, string cardNumber, string expiryDate, int cvvNum, string adress)
        {
            List<Cart> cartItems = cartService.GetAllCartItemsByUserID(userId);
            DateTime dateTimeNow = DateTime.UtcNow + TimeSpan.FromHours(3);
            List<Product> productsInCart = productService.GetAllProductsInCart(cartItems);
            decimal totalPrice = cartService.CalculateCartTotalPrice(cartItems, productsInCart);

            Order order = new Order(name, cardNumber, expiryDate, cvvNum, adress, userId, totalPrice, dateTimeNow);
            context.Order.Add(order);
            context.SaveChanges();

            context.Entry(order).Reload();
            string orderID = order.OrderID.ToString();
            cartService.UpdateCartItemsByUserID(userId, orderID);
        }

        public List<Order> GetAllOrdersByUserID(string usedID)
        {
            List<Order> orders = context.Order.Where(p => p.UserID == usedID).OrderByDescending(p => p.OrderTime).ToList();
            return orders;
        }

        public Order GetOrderByID(string orderID)
        {
            Order order = context.Order.First(p => p.OrderID == orderID);
            return order;
        }
    }
}
