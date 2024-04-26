using TechStore.Models.Entities;

namespace TechStore.Models.ViewModels
{
    public class OrdersViewModel
    {
        public List<Cart> Carts { get; set; }
        public List<Product> Products { get; set; }
        public Payment Payment { get; set; }



    }
}
