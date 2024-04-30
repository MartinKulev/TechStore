using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class Promotion
    {
        public Promotion() { }

        public Promotion(int productID, decimal newPrice)
        {
            ProductID = productID;
            NewPrice = newPrice;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        public decimal NewPrice { get; set; }
    }
}
