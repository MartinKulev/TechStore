using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels
{
    public class HomepageViewModel
    {
        public List<Promotion> Promotions { get; set; }
        public List<Product> Products { get; set; }
    }
}
