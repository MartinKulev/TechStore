using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class ReviewController : Controller
    {
        private CategoryService categoryService;
        private ReviewService reviewService;

        public ReviewController(CategoryService categoryService, ReviewService reviewService)
        {
            this.categoryService = categoryService;
            this.reviewService = reviewService;
        }


        [HttpPost]
        public IActionResult CreatedReview(int productID, int rating, string comment)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            reviewService.CreateReview(productID, userId, rating, comment);
            TempData["Message"] = "Successfully posted a review!";
            return RedirectToAction("Product", "Tech", new { productId = productID });
        }
    }
}
