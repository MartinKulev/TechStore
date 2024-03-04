using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models.Entities
{
    public class Cart
    {
        public Cart() { }

        public Cart(int productID, int quantity, int userID)
        {
            ProductID = productID;
            Quantity = quantity;
            UserID = userID;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int UserID { get; set; }
    }
}
