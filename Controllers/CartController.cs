using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Services.Interfaces;

namespace TechStore.Controllers
{
    public class CartController : Controller
    {
        private ICartService cartService;
        private IOrderService orderService;
        private IProductService productService;

        public CartController(ICartService cartService, IOrderService orderService, IProductService productService)
        {
            this.cartService = cartService;        
            this.orderService = orderService;
            this.productService = productService;
        }


        [HttpPost]
        public async Task<IActionResult> ProductAddedToCart(int productID)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await cartService.AddProductToCartAsync(userID, productID);
            }
            else
            {
                List<int> productIDs = new List<int>();
                if (TempData["Products"] != null)
                {
                    productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                }
                productIDs.Add(productID);
                TempData["Products"] = productIDs;
            }
            TempData["Message"] = "Product added to cart.";
            return RedirectToAction("Cart", "Tech");
        }


        [HttpPost]
        public async Task<IActionResult> ProductRemovedFromCart(int productID)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await cartService.RemoveProductFromCartAsync(userID, productID);
            }
            else
            {
                List<int> productIDs = new List<int>();
                if (TempData["Products"] != null)
                {
                    productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                }
                productIDs.Remove(productID);
                TempData["Products"] = productIDs;
            }
            TempData["Message"] = "Product removed from cart.";
            return RedirectToAction("Cart", "Tech");
        }
    }
}
