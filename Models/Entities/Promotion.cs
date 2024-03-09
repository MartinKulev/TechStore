using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models.Entities
{
    public class Promotion
    {
        public Promotion() { }

        public Promotion(decimal price, decimal newPrice, string imageURL, string description, string brand, string model)
        {
            Price = price;
            NewPrice = newPrice;
            ImageURL = imageURL;
            Description = description;
            Brand = brand;
            Model = model;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionID { get; set; }

        public decimal Price { get; set; }

        public decimal NewPrice { get; set; }

        public string ImageURL { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

    }
}
