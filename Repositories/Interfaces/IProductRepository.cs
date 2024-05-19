using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product);

        Task<List<Product>> GetProductsByCategoryNameAsync(string category);

        Task<Product> GetProductByIDAsync(int productID);

        Task<bool> IsProductDisabledAsync(int productID);

        Task<List<Product>> GetMultipleEnabledProductsByProductIDsAsync(List<int> productIDs);

        Task<List<Product>> GetMultipleProductsByProductIDsAsync(List<int> productIDs);

        Task<List<Product>> GetAllProductsInPromotionAsync();

        Task UpdateProductAsync(Product product);
    }
}
