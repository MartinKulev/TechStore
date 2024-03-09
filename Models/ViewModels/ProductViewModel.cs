using TechStore.Models.Entities;

namespace TechStore.Models.ViewModels
{
    public class ProductViewModel
    {
        public List<Review> Reviews { get; set; }
        public Product Product { get; set; }
        public List<ApplicationUser> Users { get; set; }

        public List<Product> ImageURL {  get; set; }    
        public List<Product> Description { get; set; }
        public List<Product> Brand { get; set; }
        public List<Product> Model { get; set; }
        public List<Product> Price { get; set; }
        public List<Product> ProductID { get; set; }
    }
}
