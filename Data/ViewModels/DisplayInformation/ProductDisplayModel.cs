using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels.DisplayInformation
{
    public class ProductDisplayModel
    {
        public ProductDisplayModel(List<Review> reviews, Product product)
        {
            Reviews = reviews;
            Product = product;
        }

        public List<Review> Reviews { get; set; }

        public Product Product { get; set; }
    }
}
