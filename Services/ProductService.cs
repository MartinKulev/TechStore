using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;
using TechStore.Services.Interfaces;

namespace TechStore.Services
{
    public class ProductService : IProductService
    {
        private ICartService cartService;
        private IProductRepository productRepository;

        public ProductService(ICartService cartService, IProductRepository productRepository)
        {
            this.cartService = cartService;
            this.productRepository = productRepository;
        }

        public ProductService(IProductRepository productRepository, ICartService cartService)
        {
            this.productRepository = productRepository;
            this.cartService = cartService;
        }

        public async Task CreateProductAsync(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            Product product = new Product(imageURL, categoryName, description, brand, model, price);
            await productRepository.CreateProductAsync(product);
        }

        public async Task DeleteProductAsync(int productID)
        {
            Product product = await productRepository.GetProductByIDAsync(productID);
            product.isDisabled = true;
            await productRepository.UpdateProductAsync(product);
            await cartService.DeleteCartsWithDeletedProductAsync(productID);
        }

        public async Task DeleteAllProductsByCategoryNameAsync(string categoryName)
        {
            List<Product> products = await productRepository.GetProductsByCategoryNameAsync(categoryName);
            foreach (Product product in products)
            {
                product.isDisabled = true;
                await productRepository.UpdateProductAsync(product);
            }
        }

        public async Task<List<Product>> GetProductsByCategoryNameAsync(string categoryName)
        {
            return await productRepository.GetProductsByCategoryNameAsync(categoryName);
        }

        public async Task<Product> GetProductByIDAsync(int productID)
        {
            return await productRepository.GetProductByIDAsync(productID);
        }

        public async Task<List<int>> RemoveDisabledProductsIDsAsync(List<int> productIDs)
        {
            List<int> productIDsToRemove = new List<int>();
            foreach (int productID in productIDs)
            {
                bool isDisabled = await productRepository.IsProductDisabledAsync(productID);
                if (isDisabled)
                {
                    productIDsToRemove.Add(productID);
                }
            }
            foreach (int productIDToRemove in productIDsToRemove)
            {
                productIDs.Remove(productIDToRemove);
            }
            return productIDs;
        }

        public async Task<List<Product>> GetAllProductsInCartAsync(List<Cart> carts)
        {
            List<int> productIDs = carts.Select(cart => cart.ProductID).ToList();
            return await productRepository.GetMultipleEnabledProductsByProductIDsAsync(productIDs);
        }

        public async Task<List<Product>> GetAllProductsInOrderAsync(List<Cart> carts)
        {
            List<int> productIDs = carts.Select(cart => cart.ProductID).ToList();
            return await productRepository.GetMultipleProductsByProductIDsAsync(productIDs);
        }

        public async Task CreatePromotionAsync(decimal newPrice, int productID)
        {
            Product product = await productRepository.GetProductByIDAsync(productID);
            product.IsInPromotion = true;
            product.NewPrice = newPrice;
            await productRepository.UpdateProductAsync(product);
        }

        public async Task RevertPromotionAsync(int productID)
        {
            Product product = await productRepository.GetProductByIDAsync(productID);
            product.IsInPromotion = false;
            product.NewPrice = 0;
            await productRepository.UpdateProductAsync(product);
        }

        public async Task<List<Product>> GetAllProductsInPromotionAsync()
        {
            return await productRepository.GetAllProductsInPromotionAsync();
        }
    }
}
