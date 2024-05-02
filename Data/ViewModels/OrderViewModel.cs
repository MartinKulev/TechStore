using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels
{
    public class OrderViewModel
    {
        public List<Cart> Carts { get; set; }
        public List<Product> Products { get; set; }
        public Order Order { get; set; }
    }
}
