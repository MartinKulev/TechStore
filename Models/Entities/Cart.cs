using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models.Entities
{
    public class Cart
    {
        public Cart() { }

        public Cart(int productID, int quantity, int userID, string description, string imageURL, decimal price)
        {
            ProductID = productID;
            Quantity = quantity;
            UserID = userID;
            Description = description;
            ImageURL = imageURL;
            Price = price;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int UserID { get; set; }

        [ForeignKey(nameof(Product))]
        public string Description { get; set; }

        [ForeignKey(nameof(Product))]
        public string ImageURL { get; set; }

        [ForeignKey(nameof(Product))]
        public decimal Price { get; set; }
    }
}
