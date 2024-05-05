using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Review
    {
        public Review() { }

        public Review(int productID, string userID, int rating, string comment)
        {     
            ProductID = productID;
            UserID = userID;
            Rating = rating;
            Comment = comment;
            CreatedDate = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }


        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }

        public DateTime CreatedDate { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
