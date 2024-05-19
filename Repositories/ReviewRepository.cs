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

        public async Task CreateReviewAsync(Review review)
        {
            await context.AddAsync(review);
            await context.SaveChangesAsync();
        }

        public async Task<List<Review>> GetAllReviewsByProductIDAsync(int productID)
        {
            return await context.Review.Where(r => r.ProductID == productID).Include(r => r.User).ToListAsync();
        }
    }
}
