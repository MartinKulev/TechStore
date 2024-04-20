using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models.Entities
{
    public class Order
    {
        public Order() { }

        public Order(int productID, int quantity, string userID, int cardNum, DateTime orderTime, decimal totalPrice)
        {
            ProductID = productID;
            Quantity = quantity;
            UserID = userID;
            CardNum = cardNum;
            OrderTime = orderTime;
            TotalPrice = totalPrice;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }

        [ForeignKey(nameof(Payment))]
        public int CardNum { get; set; }

        public DateTime OrderTime { get; set; }

        public decimal TotalPrice { get; set; }

    }

}
