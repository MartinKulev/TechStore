using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public int ProductId { get; set; }

        [StringLength(100)] 
        public string ProductName { get; set; }

        [StringLength(200)] 
        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [StringLength(100)] 
        public string CompanyName { get; set; } //Brand, instead of CompanyName?

        [ForeignKey(nameof(Category))] 
        public int CategoryId { get; set; } //We need Category name only(So we know in which tab to put the product)
                                            //This is the only thing we need from the Category table.

        public bool Sold { get; set; } //SoldOut, instead of Sold?

        public bool IsActive { get; set; } //Dont need this

        public DateTime Posted { get; set; }    //Also don't need
    }
}

