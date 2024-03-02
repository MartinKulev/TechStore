using Microsoft.AspNetCore.Mvc;
using TechStore.Data.Entities;
using TechStore.Services;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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
            //Category category1 = new Category("Лаптопи");
            //Category category2 = new Category("Телефони");
            //Category category3 = new Category("Таблети");
            //Category category4 = new Category("Телевизори");
            //Category category5 = new Category("Монитори");
            //Category category6 = new Category("Клавиатури");
            //Category category7 = new Category("Мишки");
            //Category category8 = new Category("Слушалки");
            //Category category9 = new Category("Тонколони");
            //techService.AddCategory(category1);
            //techService.AddCategory(category2);
            //techService.AddCategory(category3);
            //techService.AddCategory(category4);
            //techService.AddCategory(category5);
            //techService.AddCategory(category6);
            //techService.AddCategory(category7);
            //techService.AddCategory(category8);
            //techService.AddCategory(category9);
            //List<Category> categories = techService.GetAllCategories();
            //ViewBag.ItemsList = categories;
            return View();
        }

        public IActionResult Laptops()
        {
            List<Product> products = techService.GetProductsByCategories("Лаптопи");
            return View(products);
        }

        public IActionResult Smartphones()
        {
            List<Product> products = techService.GetProductsByCategories("Телефони");
            return View(products);
        }

        public IActionResult Tablets()
        {
            List<Product> products = techService.GetProductsByCategories("Таблети");
            return View(products);
        }

        public IActionResult TVs()
        {
            List<Product> products = techService.GetProductsByCategories("Телевизори");
            return View(products);
        }

        public IActionResult Monitors()
        {
            List<Product> products = techService.GetProductsByCategories("Монитори");
            return View(products);
        }

        public IActionResult Keyboards()
        {
            List<Product> products = techService.GetProductsByCategories("Клавиатури");
            return View(products);
        }

        public IActionResult Mice()
        {
            List<Product> products = techService.GetProductsByCategories("Мишки");
            return View(products);
        }

        public IActionResult Headphones()
        {
            List<Product> products = techService.GetProductsByCategories("Слушалки");
            return View(products);
        }

        public IActionResult Speakers()
        {
            List<Product> products = techService.GetProductsByCategories("Тонколони");
            return View(products);
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

        [HttpPost]
        [Route("{productID}/Product")]
        public IActionResult Product(int productID)
        {
            Product product = techService.GetProductByID(productID);
            return View(product);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyAddedProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            Product product = new Product(imageURL, categoryName, description, brand, model, price);
            techService.AddProduct(product);
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulPayment(string name, string cardNumber, string expiryDate, int cvvNum, string adress)
        {
            Payment payment = new Payment(name, cardNumber, expiryDate, cvvNum, adress);
            techService.AddPayment(payment);
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
