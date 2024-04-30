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
            return context.Product.ToList();
        }

        public List<Product> GetProductsByCategory(string category)
        {
            if (context.Product.Any(p => p.CategoryName == category))
            {
                List<Product> products = context.Product.Where(p => p.CategoryName == category).OrderBy(p => p.Price).ToList();
                return products;
            }
            else
            {
                return new List<Product>();
            }
        }

        public Product GetProductByID(int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            return product;
        }


    }
}
