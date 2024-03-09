    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace TechStore.Models.Entities
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
        }
    }

