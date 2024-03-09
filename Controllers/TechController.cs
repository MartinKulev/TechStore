using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TechStore.Models.Entities;
using TechStore.Models.ViewModels;
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
            List<Review> reviews = techService.GetAllReviews(productID);
            List<ApplicationUser> users = techService.GetAllUsers();

            var viewModel = new ProductViewModel
            {
                Reviews = reviews,
                Users = users,
                Product = product
            };

            return View(viewModel);
        }

        public IActionResult AdministrationPanel()
        {
            List<Promocode> promocodes = techService.GetAllPromocodes();
            List<ApplicationUser> users = techService.GetAllUsers();

            var viewModel = new AdminPanelViewModel
            {
                Promocodes = promocodes,
                Users = users
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SuccessfulyCreatedPromocode(string promocodeName, decimal discount)
        {

            Promocode promocode = new Promocode(promocodeName, discount);
            techService.CreatePromocode(promocode);
            return View();
        }

        [HttpPost]
        [Route("{promocodeName}/SuccessfulyDeletedPromocode")]
        public IActionResult SuccessfulyDeletedPromocode(string promocodeName)
        {
            techService.RemovePromocode(promocodeName);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyCreatedUser(string firstName, string lastName, string email, string phoneNumber, string password, string confirmPassword)
        {
            //ApplicationUser user = new ApplicationUser();
            //techService.CreateUser(user);
            return View();
        }

        [HttpPost]
        [Route("{userID}/SuccessfulyDeletedUser")]
        public IActionResult SuccessfulyDeletedUser(string userID)
        {
            techService.RemoveUser(userID);
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

        public IActionResult SuccessfulRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyAddedReview(string reviewText, int rate)
        {
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyDeletedProduct(int productID)
        {
            techService.RemoveProduct(productID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyAddedPromotion(decimal newPrice, int productID)
        {
            techService.AddPromotion(newPrice, productID);
            techService.RemoveProduct(productID);
            return View();
        }
        [HttpPost]
        public IActionResult SuccessfulyAddedReview(int productId, int rate, string reviewText)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            techService.AddReview(productId, userId, rate, reviewText);

            return RedirectToAction("Product", new { productID = productId });
        }

    }
}
