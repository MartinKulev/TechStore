using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Data.ViewModels;
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
        public async Task<IActionResult> CreatedReview(int productID, ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await reviewService.CreateReviewAsync(productID, userID, viewModel.CreateReviewModel.Rating, viewModel.CreateReviewModel.Comment);
                TempData["Message"] = "Successfully posted a review!";
                return RedirectToAction("Product", "Tech", new { productId = productID });
            }
            else
            {
                return RedirectToAction("Product", "Tech", new { productID = productID });
            }
        }
    }
}
