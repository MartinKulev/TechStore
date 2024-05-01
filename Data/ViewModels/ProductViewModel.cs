using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels
{
    public class ProductViewModel
    {
        public List<Review> Reviews { get; set; }
        public Product Product { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
