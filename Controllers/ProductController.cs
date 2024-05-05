using Microsoft.AspNetCore.Mvc;
using TechStore.Data.Entities;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class ProductController : Controller
    {
        private ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }


        [HttpPost]
        public IActionResult CreatedProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            productService.CreateProduct(imageURL, categoryName, description, brand, model, price);
            TempData["Message"] = "Successfully added a product!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public IActionResult DeletedProduct(int productID)
        {
            productService.DeleteProduct(productID);
            TempData["Message"] = "Successfully deleted a product!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public IActionResult CreatedPromotion(decimal newPrice, int productID)
        {
            productService.CreatePromotion(newPrice, productID);
            TempData["Message"] = "Successfully added a promotion!";
            return RedirectToAction("Product", "Tech", new { productID = productID });
        }


        [HttpPost]
        public IActionResult RevertedPromotion(int productID)
        {
            productService.RevertPromotion(productID);
            TempData["Message"] = "Successfully reverted a promotion!";
            return RedirectToAction("Product", "Tech", new { productID = productID });
        }
    }
}
