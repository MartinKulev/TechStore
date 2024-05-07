using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private TechStoreDbContext context;

        public ReviewRepository(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void CreateReview(Review review)
        {
            context.Add(review);
            context.SaveChanges();
        }

        public List<Review> GetAllReviewsByProductID(int productID)
        {
            List<Review> reviews = context.Review.Where(p => p.ProductID == productID).Include(p => p.User).ToList();
            return reviews;
        }
    }
}
