using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class Payment
    {
        public Payment() { } //When a user pays a cart we will count every information they give us as correct and every time a "succesful payment" windows will
        // appear(We cant check if the information they give us is correct) Therefore why do we need a database table for the user's information?

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
