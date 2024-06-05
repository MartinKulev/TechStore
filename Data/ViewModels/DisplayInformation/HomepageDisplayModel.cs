using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels.DisplayInformation
{
    public class HomepageDisplayModel
    {
        public HomepageDisplayModel(List<Product> products)
        {
            Products = products;
        }

        public List<Product> Products { get; set; }
    }
}
