using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        private TechStoreDbContext context;

        public ReviewRepository(TechStoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Review>> GetAllReviewsByProductIDAsync(int productID)
        {
            return await context.Reviews.Where(r => r.ProductID == productID).Include(r => r.User).ToListAsync();
        }
    }
}
