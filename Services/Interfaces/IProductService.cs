using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IProductService
    {
        void CreateProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price);

        void DeleteProduct(int productID);

        void DeleteAllProductsByCategoryName(string categoryName);

        List<Product> GetProductsByCategoryName(string categoryName);

        Product GetProductByID(int productID);

        List<int> RemoveDisabledProductsIDs(List<int> productIDs);

        List<Product> GetAllProductsInCart(List<Cart> carts);

        List<Product> GetAllProductsInOrder(List<Cart> carts);

        void CreatePromotion(decimal newPrice, int productID);

        void RevertPromotion(int productID);

        List<Product> GetAllProductsInPromotion();
    }
}
