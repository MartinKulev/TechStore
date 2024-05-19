using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Product
    {
        public Product() { }

        public Product(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            ImageURL = imageURL;
            CategoryName = categoryName;
            Description = description;
            Brand = brand;
            Model = model;
            Price = price;
            IsDisabled = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [ForeignKey(nameof(Category))]
        public string CategoryName { get; set; }

        public string ImageURL { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public bool IsInPromotion { get; set; }

        public decimal NewPrice { get; set; }

        public bool IsDisabled { get; set; }
    }
}

