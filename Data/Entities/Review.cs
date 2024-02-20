using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Review
    {
        public Review() { } 

        public Review(int reviewID, int rating, string comment, int productID, int userID, DateTime createdDate)
        {
            ReviewID = reviewID;
            Rating = rating;
            Comment = comment;
            ProductID = productID;
            UserID = userID;
            CreatedDate = createdDate;
        }

        [Key]
        public int ReviewID { get; set;}

        public int Rating { get; set; }

        public string Comment { get; set; }

        [ForeignKey(nameof(Product))] 
        public int ProductID { get; set; }

        [ForeignKey(nameof(User))] 
        public int UserID { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}

