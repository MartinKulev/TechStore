using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class ReviewController : Controller
    {
        private ReviewService reviewService;

        public ReviewController(ReviewService reviewService)
        {
            this.reviewService = reviewService;
        }


        [HttpPost]
        public IActionResult CreatedReview(int productID, int rating, string comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            reviewService.CreateReview(productID, userId, rating, comment);
            TempData["Message"] = "Successfully posted a review!";
            return RedirectToAction("Product", "Tech", new { productId = productID });
        }
    }
}
