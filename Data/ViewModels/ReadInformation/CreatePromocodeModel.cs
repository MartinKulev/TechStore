using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.ViewModels.ReadInformation
{
    public class CreatePromocodeModel
    {
        public CreatePromocodeModel() { }

        [MaxLength(50)]
        [Required]
        public string PromocodeName { get; set; }

        [MaxLength(3)]
        [Required]
        public int Discount { get; set; }
    }
}
