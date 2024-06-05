using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.ViewModels.ReadInformation
{
    public class ApplyPromocodeModel
    {
        public ApplyPromocodeModel() { }

        [MaxLength(100)]
        [Required]
        public string PromocodeName { get; set; }
    }
}
