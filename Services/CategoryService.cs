using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class CategoryService
    {
        private TechStoreDbContext context;
        private ProductService productService;

        public CategoryService(TechStoreDbContext context, ProductService productService)
        {
            this.context = context;
            this.productService = productService;
        }

        public void CreateCategory(string categoryName)
        {
            Category category = new Category(categoryName);
            context.Category.Add(category);
            context.SaveChanges();
        }

        public void DeleteCategory(string categoryName)
        {
            Category category = context.Category.First(p => p.CategoryName == categoryName);
            context.Category.Remove(category);
            context.SaveChanges();
            List<int> products = context.Product.Where(p => p.CategoryName == categoryName).Select(p => p.ProductID).ToList();
            foreach (var product in products)
            {
                productService.DeleteProduct(product);
            }
        }

        public void EditCategory(int categoryID, string newCategoryName)
        {
            Category category = GetCategoryByID(categoryID);
            if (newCategoryName != null)
            {
                category.CategoryName = newCategoryName;
            }

            context.Update(category);
            context.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = context.Category.ToList();
            return categories;
        }

        public Category GetCategoryByID(int categoryID)
        {
            Category category = context.Category.First(p => p.CategoryID == categoryID);
            return category;
        }
    }
}
