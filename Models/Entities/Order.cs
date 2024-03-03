using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models.Entities
{
    public class Order
    {
        public Order() { }

        public Order(int productID, int quantity, int userID, int cardNum, DateTime orderTime)
        {
            ProductID = productID;
            Quantity = quantity;
            UserID = userID;
            CardNum = cardNum;
            OrderTime = orderTime;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

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
