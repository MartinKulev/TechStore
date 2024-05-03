using Microsoft.AspNetCore.Mvc;
using TechStore.Data.Entities;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class ProductController : Controller
    {
        private CategoryService categoryService;
        private ProductService productService;

        public ProductController(CategoryService categoryService, ProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }


        [HttpPost]
        public IActionResult CreatedProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            Product product = new Product(imageURL, categoryName, description, brand, model, price);
            productService.CreateProduct(product);
            TempData["Message"] = "Successfully added a product!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public IActionResult DeletedProduct(int productID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            productService.DeleteProduct(productID);
            TempData["Message"] = "Successfully deleted a product!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public IActionResult CreatedPromotion(decimal newPrice, int productID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            productService.CreatePromotion(newPrice, productID);
            TempData["Message"] = "Successfully added a promotion!";
            return RedirectToAction("Product", "Tech", new { productID = productID });
        }


        [HttpPost]
        public IActionResult RevertedPromotion(int productID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            productService.RevertPromotion(productID);
            TempData["Message"] = "Successfully reverted a promotion!";
            return RedirectToAction("Product", "Tech", new { productID = productID });
        }
    }
}
