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

        public void CreateProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            Product product = new Product(imageURL, categoryName, description, brand, model, price);
            productRepository.CreateProduct(product);
        }

        public void DeleteProduct(int productID)
        {
            Product product = productRepository.GetProductByID(productID);
            product.isDisabled = true;//Note: Product is never truly deleted. It's only disabled, so it still shown in past orders Orders
            productRepository.UpdateProduct(product);
            cartService.DeleteCartsWithDeletedProduct(productID);        
        }

        public void DeleteAllProductsByCategoryName(string categoryName)
        {
            List<Product> products = productRepository.GetProductsByCategoryName(categoryName);
            foreach (Product product in products)
            {
                product.isDisabled = true;
                productRepository.UpdateProduct(product);
            }
        }

        public List<Product> GetProductsByCategoryName(string categoryName)
        {
            List<Product> products = productRepository.GetProductsByCategoryName(categoryName);
            return products;
        }
        
        public Product GetProductByID(int productID)
        {
            Product product = productRepository.GetProductByID(productID);
            return product;
        }

        public List<int> RemoveDisabledProductsIDs(List<int> productIDs)
        {
            List<int> productIDsToRemove = new List<int>();
            foreach (int productID in productIDs)
            {
                bool isDisabled = productRepository.IsProductDisabled(productID);
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

        public List<Product> GetAllProductsInCart(List<Cart> carts)
        {
            List<int> productIDs = carts.Select(cart => cart.ProductID).ToList();
            List<Product> products = productRepository.GetMultipleEnabledProductsByProductIDs(productIDs);
            return products;
        }

        public List<Product> GetAllProductsInOrder(List<Cart> carts)
        {
            List<int> productIDs = carts.Select(cart => cart.ProductID).ToList();
            List<Product> products = productRepository.GetMultipleProductsByProductIDs(productIDs);
            return products;
        }

        public void CreatePromotion(decimal newPrice, int productID)
        {
            Product product = productRepository.GetProductByID(productID);
            product.IsInPromotion = true;
            product.NewPrice = newPrice;
            productRepository.UpdateProduct(product);
        }

        public void RevertPromotion(int productID)
        {
            Product product = productRepository.GetProductByID(productID);
            product.IsInPromotion = false;
            product.NewPrice = 0;
            productRepository.UpdateProduct(product);
        }

        public List<Product> GetAllProductsInPromotion()
        {
            List<Product> products = productRepository.GetAllProductsInPromotion();
            return products;
        }
    }
}
