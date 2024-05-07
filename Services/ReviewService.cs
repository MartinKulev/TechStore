using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;
using TechStore.Services.Interfaces;

namespace TechStore.Services
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public void CreateReview(int productID, string userID, int rating, string comment)
        {
            Review review = new Review(productID, userID, rating, comment);
            reviewRepository.CreateReview(review);
        }

        public List<Review> GetAllReviewsByProductID(int productID)
        {
            List<Review> reviews = reviewRepository.GetAllReviewsByProductID(productID);
            return reviews;
        }
    }
}
