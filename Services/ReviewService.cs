using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class ReviewService
    {
        private TechStoreDbContext context;

        public ReviewService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void CreateReview(int productId, string userId, int rating, string comment)
        {
            Review review = new Review(productId, userId, rating, comment);
            context.Review.Add(review);
            context.SaveChanges();
        }

        public List<Review> GetAllReviewsByProductID(int productId)
        {
            List<Review> reviews = context.Review.Where(p => p.ProductID == productId).Include(p => p.User).ToList();
            return reviews;
        }
    }
}
