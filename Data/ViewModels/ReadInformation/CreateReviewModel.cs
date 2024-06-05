using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.ViewModels.ReadInformation
{
    public class CreateReviewModel
    {
        public CreateReviewModel() { }

        [MaxLength(100)]
        [Required]
        public string Comment { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }
    }
}
