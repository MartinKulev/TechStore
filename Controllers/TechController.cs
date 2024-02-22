using Microsoft.AspNetCore.Mvc;
using TechStore.Data.Entities;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class TechController : Controller
    {
        private TechService techService;

        public TechController(TechService techService)
        {
            this.techService = techService;
        }

        public IActionResult Homepage()
        {
            Category category1 = new Category("Лаптопи");
            Category category2 = new Category("Телефони");
            Category category3 = new Category("Таблети");
            Category category4 = new Category("Телевизори");
            Category category5 = new Category("Монитори");
            Category category6 = new Category("Клавиатури");
            Category category7 = new Category("Мишки");
            Category category8 = new Category("Слушалки");
            Category category9 = new Category("Тонколони");
            techService.AddCategory(category1);
            techService.AddCategory(category2);
            techService.AddCategory(category3);
            techService.AddCategory(category4);
            techService.AddCategory(category5);
            techService.AddCategory(category6);
            techService.AddCategory(category7);
            techService.AddCategory(category8);
            techService.AddCategory(category9);
            return View();
        }

        public IActionResult Laptops()
        {
            return View();
        }

        public IActionResult Smartphones()
        {
            return View();
        }

        public IActionResult Tablets()
        {
            return View();
        }

        public IActionResult TVs()
        {
            return View();
        }

        public IActionResult Monitors()
        {
            return View();
        }

        public IActionResult Keyboards()
        {
            return View();
        }

        public IActionResult Mice()
        {
            return View();
        }

        public IActionResult Headphones()
        {
            return View();
        }

        public IActionResult Speakers()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SuccesfulyAddedProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            Product product = new Product(imageURL, categoryName, description, brand, model, price);
            techService.AddProduct(product);
            return View();
        }
    }
}
