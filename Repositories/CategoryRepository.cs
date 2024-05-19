using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public class CategoryRepository : ICategoryRepository
    {
        private TechStoreDbContext context;

        public CategoryRepository(TechStoreDbContext context)
        {
            this.context = context;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await context.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByCategoryNameAsync(string categoryName)
        {
            return await context.Categories.FirstAsync(p => p.CategoryName == categoryName);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            context.Update(category);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            context.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
