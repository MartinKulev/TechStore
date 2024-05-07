using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        void CreateReview(Review review);

        List<Review> GetAllReviewsByProductID(int productID);
    }
}
