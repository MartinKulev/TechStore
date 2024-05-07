using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;
using TechStore.Services.Interfaces;

namespace TechStore.Services
{
    public class CategoryService : ICategoryService
    {
        private IProductService productService;
        private ICategoryRepository categoryRepository;

        public CategoryService(IProductService productService, ICategoryRepository categoryRepository)
        {
            this.productService = productService;
            this.categoryRepository = categoryRepository;
        }

        public void CreateCategory(string categoryName)
        {
            Category category = new Category(categoryName);
            categoryRepository.CreateCategory(category);
        }

        public void DeleteCategory(string categoryName)
        {
            Category category = categoryRepository.GetCategoryByCategoryName(categoryName);
            categoryRepository.DeleteCategory(category);
            productService.DeleteAllProductsByCategoryName(categoryName);
        }

        public void EditCategory(string categoryName, string newCategoryName)
        {
            if (newCategoryName != null)
            {
                Category category = categoryRepository.GetCategoryByCategoryName(categoryName);
                category.CategoryName = newCategoryName;
                categoryRepository.UpdateCategory(category);
            }
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = categoryRepository.GetAllCategories();
            return categories;
        }
    }
}
