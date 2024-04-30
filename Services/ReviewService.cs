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
            var review = new Review
            {
                ProductID = productId,
                UserID = userId,
                Rating = rating,
                Comment = comment,
                CreatedDate = DateTime.Now
            };

            context.Review.Add(review);
            context.SaveChanges();
        }

        public List<Review> GetAllReviews(int productId)
        {
            List<Review> reviews = context.Review.Where(r => r.ProductID == productId).Include(r => r.User).ToList();
            return reviews;
        }


    }
}
