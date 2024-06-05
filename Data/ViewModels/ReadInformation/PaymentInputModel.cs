using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.ViewModels.ReadInformation
{
    public class PaymentInputModel
    {
        public PaymentInputModel() { }

        [MaxLength(70)]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.CreditCard)]
        [Length(19, 19)]
        [Required]
        public string CardNum { get; set; }

        [Length(5, 5)]
        [Required]
        public string ExpiryDate { get; set; }

        [Range(100, 999)]
        [Required]
        public int CvvNum { get; set; }

        [MaxLength(200)]
        [Required]
        public string Address { get; set; }
    }
}
