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

        public async Task CreateReviewAsync(int productID, string userID, int rating, string comment)
        {
            Review review = new Review(productID, userID, rating, comment);
            await reviewRepository.CreateReviewAsync(review);
        }

        public async Task<List<Review>> GetAllReviewsByProductIDAsync(int productID)
        {
            return await reviewRepository.GetAllReviewsByProductIDAsync(productID);
        }
    }
}
