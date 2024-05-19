using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IReviewService
    {
        Task CreateReviewAsync(int productID, string userID, int rating, string comment);

        Task<List<Review>> GetAllReviewsByProductIDAsync(int productID);
    }
}
