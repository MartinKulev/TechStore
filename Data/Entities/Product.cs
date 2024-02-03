using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class Product
    {
        public Product() { }

        public Product(int productId, string productName, string shortDescription, string longDescription, decimal price, int quantity, string companyName, int categoryId, bool sold, bool isActive, DateTime posted)
        {
            ProductId = productId;
            ProductName = productName;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            Price = price;
            Quantity = quantity;
            CompanyName = companyName;
            CategoryId = categoryId;
            Sold = sold;
            IsActive = isActive;
            Posted = posted;
        }
        public int ProductId { get; set; }

        [StringLength(100)] 
        public string ProductName { get; set; }

        [StringLength(200)] 
        public int ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [StringLength(100)] 
        public string CompanyName { get; set; }

        [ForeignKey(nameof(Category))] 
        public int CategoryId { get; set; }

        public bool Sold { get; set; }

        public bool IsActive { get; set; }

        public DateTime posted { get; set; }    
    }
}

