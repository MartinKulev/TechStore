using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Services.Interfaces;

namespace TechStore.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }


        [HttpPost]
        public async Task<IActionResult> CreatedReview(int productID, int rating, string comment)
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await reviewService.CreateReviewAsync(productID, userID, rating, comment);
            TempData["Message"] = "Successfully posted a review!";
            return RedirectToAction("Product", "Tech", new { productId = productID });
        }
    }
}
