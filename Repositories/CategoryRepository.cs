using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces;

public class CategoryRepository : ICategoryRepository
{
    private TechStoreDbContext context;

    public CategoryRepository(TechStoreDbContext context)
    {
        this.context = context;
    }

    public void CreateCategory(Category category)
    {
        context.Add(category);
        context.SaveChanges();
    }

    public List<Category> GetAllCategories()
    {
        List<Category> categories = context.Category.ToList();
        return categories;
    }

    public Category GetCategoryByCategoryName(string categoryName)
    {
        Category category = context.Category.First(p => p.CategoryName == categoryName);
        return category;
    }

    public void UpdateCategory(Category category)
    {
        context.Update(category);
        context.SaveChanges();
    }

    public void DeleteCategory(Category category)
    {
        context.Remove(category);
        context.SaveChanges();
    }
}
