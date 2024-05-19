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
        public async Task<IActionResult> CreatedCategory(string categoryName)
        {
            await categoryService.CreateCategoryAsync(categoryName);
            TempData["Message"] = "Successfully created a category!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public async Task<IActionResult> DeletedCategory(string categoryName)
        {
            await categoryService.DeleteCategoryAsync(categoryName);
            TempData["Message"] = "Successfully deleted a category!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public async Task<IActionResult> EditedCategory(string categoryName, string newCategoryName)
        {
            await categoryService.EditCategoryAsync(categoryName, newCategoryName);
            TempData["Message"] = "Successfully edited a category!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }
    }
}
