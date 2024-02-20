using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Order
    {
        public Order () { }

        public Order(int orderID, int productID, int quantity, int userID, int cardNum, DateTime orderTime)
        {
            OrderID = orderID;
            ProductID = productID;
            Quantity = quantity;
            UserID = userID;
            CardNum = cardNum;
            OrderTime = orderTime;
        }

        [Key]
        public int OrderID { get; set; } //RNG 8 cifri

        [ForeignKey(nameof(Product))] 
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(User))]
        public int UserID { get; set; }

        [ForeignKey(nameof(Payment))]
        public int CardNum { get; set; }

        public DateTime OrderTime { get; set; }

    }
    
}
