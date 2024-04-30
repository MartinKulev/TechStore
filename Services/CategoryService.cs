using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class CategoryService
    {
        private TechStoreDbContext context;

        public CategoryService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void AddCategory(Category category)
        {
            if (!context.Category.Contains(category))
            {
                context.Category.Add(category);
                context.SaveChanges();
            }
        }

        public void DeleteCategory(string categoryName)//
        {
            Category category = context.Category.First(p => p.CategoryName == categoryName);
            context.Category.Remove(category);
            DeleteAllProductsFromACategory(categoryName);
            context.SaveChanges();
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
            Category category = context.Category.First(p => p.CategoryId == categoryID);
            return category;
        }

        public void DeleteAllProductsFromACategory(string categoryName)//
        {
            List<Product> products = context.Product.Where(p => p.CategoryName == categoryName).ToList();
            foreach (var product in products)
            {
                context.Product.Remove(product);
                if (product.IsInPromotion)
                {
                    Promotion promotion = context.Promotion.First(p => p.ProductID == product.ProductID);
                    context.Promotion.Remove(promotion);
                }
            }
            context.SaveChanges();
        }



    }
}
