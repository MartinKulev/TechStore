using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private TechStoreDbContext context;

        public ProductRepository(TechStoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetProductsByCategoryNameAsync(string category)
        {
            return await context.Products.Where(p => p.CategoryName == category && !p.IsDisabled).OrderBy(p => p.Price).ToListAsync();
        }

        public async Task<Product> GetProductByIDAsync(int productID)
        {
            return await context.Products.FirstAsync(p => p.ProductID == productID);
        }

        public async Task<bool> IsProductDisabledAsync(int productID)
        {
            return await context.Products.AnyAsync(p => p.ProductID == productID && p.IsDisabled);
        }

        public async Task<List<Product>> GetAllEnabledProductsByProductIDsAsync(List<int> productIDs)
        {
            return await context.Products.Where(p => productIDs.Contains(p.ProductID) && !p.IsDisabled).ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsByProductIDsAsync(List<int> productIDs)
        {
            return await context.Products.Where(p => productIDs.Contains(p.ProductID)).ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsInPromotionAsync()
        {
            return await context.Products.Where(p => p.IsInPromotion && !p.IsDisabled).ToListAsync();
        }
    }
}
