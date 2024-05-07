using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IReviewService
    {
        void CreateReview(int productId, string userId, int rating, string comment);

        List<Review> GetAllReviewsByProductID(int productId);
    }
}
