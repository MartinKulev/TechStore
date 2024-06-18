using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private TechStoreDbContext context;

        public CategoryRepository(TechStoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Category> GetCategoryByCategoryNameAsync(string categoryName)
        {
            return await context.Categories.FirstAsync(p => p.CategoryName == categoryName);
        }
    }
}
