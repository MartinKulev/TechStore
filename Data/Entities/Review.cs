using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Review
    {
        public Review() { }

        public Review(int rating, string comment, int productID, string userID, DateTime createdDate)
        {
            Rating = rating;
            Comment = comment;
            ProductID = productID;
            UserID = userID;
            CreatedDate = createdDate;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewID { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }

        public DateTime CreatedDate { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
