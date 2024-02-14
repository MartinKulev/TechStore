using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Wishlist //We don't need a whishlist, when we have "cart" imo
    {
        public Wishlist() { }

        public Wishlist(int wishlistId, int productId, int userId) 
        {
            WishlistId = wishlistId;
            ProductId = productId;
            UserId = userId;
        }
        [Key]
        public int WishlistId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [ForeignKey(nameof(User))] 
        public int UserId { get; set; } 

    }
}
