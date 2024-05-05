using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class ProductService
    {
        private TechStoreDbContext context;

        public ProductService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void CreateProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            Product product = new Product(imageURL, categoryName, description, brand, model, price);
            context.Product.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            product.isDisabled = true;//Note: Product is never truly deleted. It's only disabled, so it still shown in past orders Orders
            context.Product.Update(product);
            var cartsToRemove = context.Cart.Where(p => p.ProductID == product.ProductID && !p.IsOrdered).ToList();
            context.Cart.RemoveRange(cartsToRemove);
            context.SaveChanges();
        }

        public List<Product> GetProductsByCategory(string category)
        {
            List<Product> products = context.Product.Where(p => p.CategoryName == category && !p.isDisabled).OrderBy(p => p.Price).ToList();
            return products;
        }
        
        public Product GetProductByID(int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            return product;
        }

        public List<int> GetAllEnabledProductsIDs(List<int> productIDs)
        {
            List<int> enabledProductIDs = context.Product.Where(p => productIDs.Contains(p.ProductID) && !p.isDisabled).Select(p => p.ProductID).ToList();
            return enabledProductIDs;
        }

        public List<Product> GetAllProductsInCart(List<Cart> carts)
        {
            List<int> productIDs = carts.Select(cart => cart.ProductID).ToList();
            List<Product> products = context.Product.Where(p => productIDs.Contains(p.ProductID) && !p.isDisabled).ToList();
            return products;
        }

        public List<Product> GetAllProductsInOrder(List<Cart> carts)
        {
            List<int> productIDs = carts.Select(cart => cart.ProductID).ToList();
            List<Product> products = context.Product.Where(p => productIDs.Contains(p.ProductID)).ToList();
            return products;
        }

        public void CreatePromotion(decimal newPrice, int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            product.IsInPromotion = true;
            product.NewPrice = newPrice;
            context.Update(product);
            context.SaveChanges();
        }

        public void RevertPromotion(int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            product.IsInPromotion = false;
            product.NewPrice = 0;
            context.Update(product);
            context.SaveChanges();
        }

        public List<Product> GetAllProductsInPromotion()
        {
            List<Product> products = context.Product.Where(p => p.IsInPromotion && !p.isDisabled).ToList();
            return products;
        }
    }
}
