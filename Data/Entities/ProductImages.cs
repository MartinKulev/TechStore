    using System.ComponentModel.DataAnnotations;


namespace TechStore.Data.Entities
{
    public class ProductImages
    {
        public ProductImages() { }

        public ProductImages(int imageId, string imageUrl, int productId)
        {
            ImageId = imageId;
            ImageUrl = imageUrl;
            ProductId = productId;
        }
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Product))] 
        public int ProductId { get; set; }
    }
}