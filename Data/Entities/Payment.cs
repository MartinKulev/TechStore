using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class Payment
    {
        public Payment() { }

        public Payment(int paymentId, string name, string cardNum, string expiryDate, int cvvNum, string address, string paymentMode)
        {
            PaymentId = paymentId;
            Name = name;
            CardNum = cardNum;
            ExpiryDate = expiryDate;
            CvvNum = cvvNum;
            Address = address;
            PaymentMode = paymentMode;
        }
        [Key]
        public int PaymentId { get; set; }

        [StringLength(50)] 
        public string Name { get; set; }

        [StringLength(50)] 
        public string CardNum { get; set; }

        [StringLength(50)] 
        public string ExpiryDate { get; set;}

        public int CvvNum { get; set; }

        public string Address { get; set; }

        [StringLength(50)] 
        public string PaymentMode { get; set; }
    }
}
