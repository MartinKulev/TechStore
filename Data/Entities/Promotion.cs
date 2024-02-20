using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class Promotion
    {
        public Promotion() { }

        public Promotion(int promotionID,  int productID, decimal price, decimal newPrice)
        {
            PromotionID = promotionID;
            ProductID = productID;
            Price = price;
            NewPrice = newPrice;
        }

        [Key]
        public int PromotionID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        [ForeignKey(nameof(Product))]
        public decimal Price { get; set; }

        public decimal NewPrice { get; set; }
    }
}
