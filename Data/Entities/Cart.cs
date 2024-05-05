using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Cart
    {
        public Cart() { }

        public Cart(string userID, int productID, int quantity)
        {
            UserID = userID;
            ProductID = productID;
            Quantity = quantity;
            OrderID = string.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CartID { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(Order))]
        public string OrderID { get; set; }
    }
}
