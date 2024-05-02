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

        public void CreateProduct(Product product)
        {
            context.Product.Add(product);
            context.SaveChanges();
        }

        public void RemoveProduct(int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            context.Product.Remove(product);
            context.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = context.Product.ToList();
            return products;
        }


        public List<Product> GetProductsByCategory(string category)
        {
            List<Product> products = context.Product.Where(p => p.CategoryName == category).OrderBy(p => p.Price).ToList();
            return products;
        }

        public void AddProductToPromotion(decimal newPrice, int productID)
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
            List<Product> products = context.Product.Where(p => p.IsInPromotion).ToList();
            return products;
        }

        public Product GetProductByID(int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            return product;
        }

        public List<Product> GetAllProductsInCart(List<Cart> carts)
        {
            List<int> productIDs = carts.Select(cart => cart.ProductID).ToList();
            List<Product> products = context.Product.Where(p => productIDs.Contains(p.ProductID)).ToList();
            return products;
        }
    }
}
