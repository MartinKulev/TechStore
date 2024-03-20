using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models.Entities
{
    public class Promocode
    {
        public Promocode() { }

        public Promocode(string promocodeName, decimal discount)
        {
            PromocodeName = promocodeName;
            Discount = discount;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromocodeId { get; set; }

        public string PromocodeName { get; set; }

        public decimal Discount { get; set; }


    }
}
