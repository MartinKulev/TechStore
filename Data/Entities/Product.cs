using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Product
    {
        public Product() { }

        public Product(int productID, string productName, string description, decimal price, int quantity, string brand, int categoryID)
        {
            ProductID = productID;
            ProductName = productName;
            Description = description;
            Price = price;
            Quantity = quantity;
            Brand = brand;
            CategoryID = categoryID;
        }

        [Key]
        public int ProductID { get; set; }

        [StringLength(100)] 
        public string ProductName { get; set; }

        [StringLength(200)] 
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [StringLength(100)] 
        public string Brand { get; set; }

        [ForeignKey(nameof(Category))] 
        public int CategoryID { get; set; } 
    }
}

