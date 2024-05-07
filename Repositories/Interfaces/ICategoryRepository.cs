using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category category);

        List<Category> GetAllCategories();

        Category GetCategoryByCategoryName(string categoryName);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);
    }
}
