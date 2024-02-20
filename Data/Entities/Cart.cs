using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Cart
    {
        public Cart() { }

        public Cart(int cartID, int productID, int quantity, int userID)
        {
            CartID = cartID;
            ProductID = productID;  
            Quantity = quantity;
            UserID = userID;
        }

        [Key]
        public int CartID { get; set;}

        [ForeignKey(nameof(Product))] 
        public int ProductID { get; set; } 

        public int Quantity { get; set;} 

        [ForeignKey(nameof(User))] 
        public int UserID { get; set;} 
    }
}
