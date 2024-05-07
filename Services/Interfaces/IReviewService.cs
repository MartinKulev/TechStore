using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IReviewService
    {
        void CreateReview(int productID, string userID, int rating, string comment);

        List<Review> GetAllReviewsByProductID(int productID);
    }
}
