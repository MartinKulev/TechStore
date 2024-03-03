using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models.Entities
{
    public class Promotion
    {
        public Promotion() { }

        public Promotion(int productID, decimal price, decimal newPrice)
        {
            ProductID = productID;
            Price = price;
            NewPrice = newPrice;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        [ForeignKey(nameof(Product))]
        public decimal Price { get; set; }

        public decimal NewPrice { get; set; }
    }
}
