using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Order
    {
        public Order() { }

        public Order(string name, string cardNum, string expiryDate, int cvvNum, string address, string userID, decimal totalPrice, DateTime orderTime)
        {
            OrderID = Guid.NewGuid().ToString();
            Name = name;
            CardNum = cardNum;
            ExpiryDate = expiryDate;
            CvvNum = cvvNum;
            Address = address;
            UserID = userID;
            TotalPrice = totalPrice;
            OrderTime = orderTime;
        }

        [Key]
        public string OrderID { get; set; }

        public string CardNum { get; set; }

        public string Name { get; set; }

        public string ExpiryDate { get; set; }

        public int CvvNum { get; set; }

        public string Address { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderTime { get; set; }
    }
}
