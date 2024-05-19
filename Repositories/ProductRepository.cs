using Microsoft.EntityFrameworkCore;
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

        public async Task CreateProductAsync(Product product)
        {
            await context.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            context.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryNameAsync(string category)
        {
            return await context.Product.Where(p => p.CategoryName == category && !p.isDisabled).OrderBy(p => p.Price).ToListAsync();
        }

        public async Task<Product> GetProductByIDAsync(int productID)
        {
            return await context.Product.FirstAsync(p => p.ProductID == productID);
        }

        public async Task<bool> IsProductDisabledAsync(int productID)
        {
            return await context.Product.AnyAsync(p => p.ProductID == productID && p.isDisabled);
        }

        public async Task<List<Product>> GetMultipleEnabledProductsByProductIDsAsync(List<int> productIDs)
        {
            return await context.Product.Where(p => productIDs.Contains(p.ProductID) && !p.isDisabled).ToListAsync();
        }

        public async Task<List<Product>> GetMultipleProductsByProductIDsAsync(List<int> productIDs)
        {
            return await context.Product.Where(p => productIDs.Contains(p.ProductID)).ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsInPromotionAsync()
        {
            return await context.Product.Where(p => p.IsInPromotion && !p.isDisabled).ToListAsync();
        }
    }
}
