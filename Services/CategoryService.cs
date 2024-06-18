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

        public async Task CreateCategoryAsync(string categoryName)
        {
            Category category = new Category(categoryName);
            await categoryRepository.CreateAsync(category);
        }

        public async Task DeleteCategoryAsync(string categoryName)
        {
            Category category = await categoryRepository.GetCategoryByCategoryNameAsync(categoryName);
            await categoryRepository.DeleteAsync(category);
            await productService.DeleteAllProductsByCategoryNameAsync(categoryName);
        }

        public async Task EditCategoryAsync(string categoryName, string newCategoryName)
        {
            if (newCategoryName != null)
            {
                Category category = await categoryRepository.GetCategoryByCategoryNameAsync(categoryName);
                category.CategoryName = newCategoryName;
                await categoryRepository.UpdateAsync(category);
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await categoryRepository.GetAllAsync();
        }
    }
}
