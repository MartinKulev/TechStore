using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(List<Review> reviews, Product product, List<ApplicationUser> users)
        {
            Reviews = reviews;
            Product = product;
            Users = users;
        }

        public List<Review> Reviews { get; set; }
        public Product Product { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
