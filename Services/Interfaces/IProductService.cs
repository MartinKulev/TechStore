using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(string imageURL, string categoryName, string description, string brand, string model, decimal price);

        Task DeleteProductAsync(int productID);

        Task DeleteAllProductsByCategoryNameAsync(string categoryName);

        Task<List<Product>> GetProductsByCategoryNameAsync(string categoryName);

        Task<Product> GetProductByIDAsync(int productID);

        Task<List<int>> RemoveDisabledProductsIDsAsync(List<int> productIDs);

        Task<List<Product>> GetAllProductsInCartAsync(List<Cart> carts);

        Task<List<Product>> GetAllProductsInOrderAsync(List<Cart> carts);

        Task CreatePromotionAsync(decimal newPrice, int productID);

        Task RevertPromotionAsync(int productID);

        Task<List<Product>> GetAllProductsInPromotionAsync();
    }
}
