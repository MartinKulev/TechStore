using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechStore.Data.Entities;
using TechStore.Services;

namespace TechStore.Filters
{
    public class TechFilter : IResultFilter
    {
        private CategoryService categoryService;

        public TechFilter(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                var tempData = controller.TempData;

                if (tempData["Products"] != null)
                {
                    var productIDs = (tempData["Products"] as IEnumerable<int>).ToList();
                    tempData["Products"] = productIDs;
                }

                List<Category> categories = categoryService.GetAllCategories();
                controller.ViewBag.CategoryList = categories;
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            
        }
    }
}
