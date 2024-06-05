using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels.DisplayInformation
{
    public class OrderDisplayModel
    {
        public OrderDisplayModel(List<Cart> carts, List<Product> products, Order order)
        {
            Carts = carts;
            Products = products;
            Order = order;
        }

        public List<Cart> Carts { get; set; }

        public List<Product> Products { get; set; }

        public Order Order { get; set; }
    }
}
