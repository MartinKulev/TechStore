using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class Wishlist
    {
        public Wishlist() { }

        public Wishlist(int wishlistId, int productId, int userId) 
        {
            WishlistId = wishlistId;
            ProductId = productId;
            UserId = userId;
        }
        public int WishlistId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [ForeignKey(nameof(User))] 
        public int UserId { get; set; } 

    }
}
