using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class Payment
    {
        public Payment() { }

        public Payment(int paymentID, string name, string cardNum, string expiryDate, int cvvNum, string address)
        {
            PaymentID = paymentID;
            Name = name;
            CardNum = cardNum;
            ExpiryDate = expiryDate;
            CvvNum = cvvNum;
            Address = address;
        }

        [Key]
        public int PaymentID { get; set; }

        [StringLength(50)] 
        public string Name { get; set; }

        [StringLength(50)] 
        public string CardNum { get; set; }

        [StringLength(50)] 
        public string ExpiryDate { get; set;}

        public int CvvNum { get; set; }

        public string Address { get; set; }
    }
}
