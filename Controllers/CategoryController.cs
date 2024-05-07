using Microsoft.AspNetCore.Mvc;
using TechStore.Services.Interfaces;

namespace TechStore.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        [HttpPost]
        public IActionResult CreatedCategory(string categoryName)
        {
            categoryService.CreateCategory(categoryName);
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
        public IActionResult EditedCategory(string categoryName, string newCategoryName)
        {
            categoryService.EditCategory(categoryName, newCategoryName);
            TempData["Message"] = "Successfully edited a category!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }
    }
}
