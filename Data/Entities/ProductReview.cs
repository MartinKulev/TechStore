using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class ProductReview
    {
        public ProductReview() { }      

        public ProductReview(int reviewId, int rating, string comment, int productId, int userId, Datetime createdDate)
        {
            ReviewId = reviewId;
            Rating = rating;
            Comment = comment;
            ProductId = productId;
            UserId = userId;
            CreatedDate = createdDate;
        }
        public int ReviewId { get; set;}

        public int Rating { get; set; }

        public string Comment { get; set; }

        [ForeignKey(nameof(Product))] 
        public int ProductId { get; set; }

        [ForeignKey(nameof(User))] 
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}

