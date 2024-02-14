    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TechStore.Data.Entities
{
    public class ProductImages
    {
        public ProductImages() { } //We can keep this table if we dont meet the minimal requirement for table count.
                                   //Otherwise imageUrl will be in "Product" table

        public ProductImages(int imageId, string imageUrl, int productId)
        {
            ImageId = imageId;
            ImageUrl = imageUrl;
            ProductId = productId;
        }
        [Key]
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Product))] 
        public int ProductId { get; set; }
    }
}