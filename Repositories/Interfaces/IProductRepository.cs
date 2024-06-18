using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetProductsByCategoryNameAsync(string category);

        Task<Product> GetProductByIDAsync(int productID);

        Task<bool> IsProductDisabledAsync(int productID);

        Task<List<Product>> GetAllEnabledProductsByProductIDsAsync(List<int> productIDs);

        Task<List<Product>> GetAllProductsByProductIDsAsync(List<int> productIDs);

        Task<List<Product>> GetAllProductsInPromotionAsync();
    }
}
