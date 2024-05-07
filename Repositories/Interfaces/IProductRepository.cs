using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public void CreateProduct(Product product);

        public List<Product> GetProductsByCategoryName(string category);

        public Product GetProductByID(int productID);

        public bool IsProductDisabled(int productID);

        public List<Product> GetMultipleEnabledProductsByProductIDs(List<int> productIDs);

        public List<Product> GetMultipleProductsByProductIDs(List<int> productIDs);

        public List<Product> GetAllProductsInPromotion();

        public void UpdateProduct(Product product);
    }
}
