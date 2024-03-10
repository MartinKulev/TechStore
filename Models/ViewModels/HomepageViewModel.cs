using TechStore.Models.Entities;

namespace TechStore.Models.ViewModels
{
    public class HomepageViewModel
    {
        public List<Promotion> Promotions { get; set; }
        public List<Product> Products { get; set; }
    }
}
