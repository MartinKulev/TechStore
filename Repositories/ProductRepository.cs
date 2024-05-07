using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private TechStoreDbContext context;

        public ProductRepository(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void CreateProduct(Product product)
        {
            context.Add(product);
            context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            context.Update(product);
            context.SaveChanges();
        }

        public List<Product> GetProductsByCategoryName(string category)
        {
            List<Product> products = context.Product.Where(p => p.CategoryName == category && !p.isDisabled).OrderBy(p => p.Price).ToList();
            return products;
        }

        public Product GetProductByID(int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            return product;
        }

        public bool IsProductDisabled(int productID)
        {
            bool isDisabled = context.Product.Any(p => p.ProductID == productID && p.isDisabled);
            return isDisabled;
        }

        public List<Product> GetMultipleEnabledProductsByProductIDs(List<int> productIDs)
        {
            List<Product> products = context.Product.Where(p => productIDs.Contains(p.ProductID) && !p.isDisabled).ToList();
            return products;
        }

        public List<Product> GetMultipleProductsByProductIDs(List<int> productIDs)
        {
            List<Product> products = context.Product.Where(p => productIDs.Contains(p.ProductID)).ToList();
            return products;
        }

        public List<Product> GetAllProductsInPromotion()
        {
            List<Product> products = context.Product.Where(p => p.IsInPromotion && !p.isDisabled).ToList();
            return products;
        }
    }
}
