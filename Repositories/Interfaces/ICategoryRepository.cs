using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Category category);

        Task<List<Category>> GetAllCategoriesAsync();

        Task<Category> GetCategoryByCategoryNameAsync(string categoryName);

        Task UpdateCategoryAsync(Category category);

        Task DeleteCategoryAsync(Category category);
    }
}
