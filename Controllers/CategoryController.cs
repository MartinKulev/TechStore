using Microsoft.AspNetCore.Mvc;
using TechStore.Data.Entities;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        [HttpPost]
        public IActionResult CreatedCategory(string categoryName)
        {
            Category category = new Category(categoryName);
            categoryService.CreateCategory(category);
            TempData["Message"] = "Successfully created a category!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public IActionResult DeletedCategory(string categoryName)
        {
            categoryService.DeleteCategory(categoryName);
            TempData["Message"] = "Successfully deleted a category!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public IActionResult EditedCategory(int categoryID, string newCategoryName)
        {
            categoryService.EditCategory(categoryID, newCategoryName);
            TempData["Message"] = "Successfully edited a category!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }
    }
}
