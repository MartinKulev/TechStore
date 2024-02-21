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
            Category category1 = new Category(1, "Лаптопи");
            Category category2 = new Category(2, "Телефони");
            Category category3 = new Category(3, "Таблети");
            Category category4 = new Category(4, "Телевизори");
            Category category5 = new Category(5, "Монитори");
            Category category6 = new Category(6, "Клавиатури");
            Category category7 = new Category(7, "Мишки");
            Category category8 = new Category(8, "Слушалки");
            Category category9 = new Category(9, "Тонколони");
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
    }
}
