using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface ICategoryService
    {
        void CreateCategory(string categoryName);

        void DeleteCategory(string categoryName);

        void EditCategory(string categoryName, string newCategoryName);

        List<Category> GetAllCategories();
    }
}
