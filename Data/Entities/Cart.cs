using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Data.Entities
{
    public class Cart
    {
        public Cart() { }

        public Cart(int cartId, int productId, int quantity, int userId)
        {
            CartId = cartId;
            ProductId = productId;  
            Quantity = quantity;
            UserId = userId;
        }
        [Key]
        public int CartId { get; set;} //Do we need CardID if we have UserID? I am assuming one user wont have multiple carts

        [ForeignKey(nameof(Product))] 
        public int ProductId { get; set; } //List<int> of productIDs instead of int?

        public int Quantity { get; set;} 

        [ForeignKey(nameof(Product))] 
        public int UserId { get; set;} //Name of Product in the foreign key?
    }
}
