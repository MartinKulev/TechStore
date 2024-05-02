using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(List<Review> reviews, Product product)
        {
            Reviews = reviews;
            Product = product;
        }

        public List<Review> Reviews { get; set; }
        public Product Product { get; set; }
    }
}
