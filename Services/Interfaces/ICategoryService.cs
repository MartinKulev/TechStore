using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(string categoryName);

        Task DeleteCategoryAsync(string categoryName);

        Task EditCategoryAsync(string categoryName, string newCategoryName);

        Task<List<Category>> GetAllCategoriesAsync();
    }
}
