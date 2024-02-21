using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Payment
    {
        public Payment() { }

        public Payment(string name, string cardNum, string expiryDate, int cvvNum, string address)
        {
            Name = name;
            CardNum = cardNum;
            ExpiryDate = expiryDate;
            CvvNum = cvvNum;
            Address = address;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
