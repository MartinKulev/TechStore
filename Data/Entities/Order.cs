using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Order
    {
        public Order () { }

        public Order(int orderDetailsId, string orderNum, int productId, int quantity, int userId, string status, int paymentId, DateTime orderTime, bool isCancel)
        {
            OrderDetailsId = orderDetailsId;
            OrderNum = orderNum;
            ProductId = productId;
            Quantity = quantity;
            UserId = userId;
            Status = status;
            PaymentId = paymentId;
            OrderTime = orderTime;
            IsCancel = isCancel;
        }
        public int OrderDetailsId { get; set; }

        public string OrderNum { get; set; }

        [ForeignKey(nameof(Product))] 
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [StringLength(255)] 
        public string Status { get; set; }

        [ForeignKey(nameof(Payment))]
        public int PaymentId { get; set; }

        public DateTime OrderTime { get; set; }

        public bool IsCancel { get; set; }
    }
    
}
