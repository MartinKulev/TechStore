using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task CreateReviewAsync(Review review);

        Task<List<Review>> GetAllReviewsByProductIDAsync(int productID);
    }
}
