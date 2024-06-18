using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<List<Review>> GetAllReviewsByProductIDAsync(int productID);
    }
}
