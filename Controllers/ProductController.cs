using Microsoft.AspNetCore.Mvc;
using TechStore.Services.Interfaces;

namespace TechStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }


        [HttpPost]
        public async Task<IActionResult> CreatedProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            await productService.CreateProductAsync(imageURL, categoryName, description, brand, model, price);
            TempData["Message"] = "Successfully added a product!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public async Task<IActionResult> DeletedProduct(int productID)
        {
            await productService.DeleteProductAsync(productID);
            TempData["Message"] = "Successfully deleted a product!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public async Task<IActionResult> CreatedPromotion(decimal newPrice, int productID)
        {
            await productService.CreatePromotionAsync(newPrice, productID);
            TempData["Message"] = "Successfully added a promotion!";
            return RedirectToAction("Product", "Tech", new { productID = productID });
        }


        [HttpPost]
        public async Task<IActionResult> RevertedPromotion(int productID)
        {
            await productService.RevertPromotionAsync(productID);
            TempData["Message"] = "Successfully reverted a promotion!";
            return RedirectToAction("Product", "Tech", new { productID = productID });
        }
    }
}
