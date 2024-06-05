using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels.DisplayInformation
{
    public class CategoryDisplayModel
    {
        public CategoryDisplayModel(List<Product> products)
        { 
            Products = products;
        }

        public List<Product> Products { get; set; }
    }
}
