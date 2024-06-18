using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetCategoryByCategoryNameAsync(string categoryName);
    }
}
