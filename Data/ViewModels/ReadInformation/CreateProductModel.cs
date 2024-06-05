using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels.ReadInformation
{
    public class CreateProductModel
    {
        public CreateProductModel() { }

        public string CategoryName { get; set; }

        [MaxLength(500)]
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageURL { get; set; }

        [MaxLength(150)]
        [Required]
        public string Description { get; set; }

        [MaxLength(28)]
        [Required]
        public string Brand { get; set; }

        [MaxLength(28)]
        [Required]
        public string Model { get; set; }

        [MaxLength(28)]
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
