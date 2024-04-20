using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models.Entities
{
    public class TempOrder
    {
        public TempOrder() { }

        public TempOrder(int productID, int quantity, string userID, DateTime orderTime)
        {
            ProductID = productID;
            Quantity = quantity;
            UserID = userID;
            OrderTime = orderTime;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }

        public DateTime OrderTime { get; set; }

    }
}


