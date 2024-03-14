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
            techService.AddАrtificiallyCategories(); //this method exists for developing purposes only
            
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            List<Promotion> promotions = techService.GetAllPromotions();
            List<Product> products = new List<Product>();
            foreach (var promotion in promotions)
            {
                Product product = techService.GetProductByID(promotion.ProductID);
                products.Add(product);
            }

            var viewModel = new HomepageViewModel
            {
                Promotions = promotions,
                Products = products
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Category(string categoryName)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            List<Product> products = techService.GetProductsByCategories(categoryName);
            return View(products);
        }

        public IActionResult ShoppingCart()
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            return View();
        }

        [HttpPost]
        [Route("{productID}/Product")]
        public IActionResult Product(int productID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            Product product = techService.GetProductByID(productID);
            List<Review> reviews = techService.GetAllReviews(productID);
            List<ApplicationUser> users = techService.GetAllUsers();
            List<Promotion> promotions = techService.GetAllPromotions();

            var viewModel = new ProductViewModel
            {
                Reviews = reviews,
                Users = users,
                Product = product,
                Promotions = promotions
            };

            return View(viewModel);
        }

        public IActionResult AdministrationPanel()
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            List<Promocode> promocodes = techService.GetAllPromocodes();
            List<ApplicationUser> users = techService.GetAllUsers();
            //List<Category> categories = techService.GetAllCategories();

            var viewModel = new AdminPanelViewModel
            {
                Promocodes = promocodes,
                Users = users,
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SuccessfulyCreatedPromocode(string promocodeName, decimal discount)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            Promocode promocode = new Promocode(promocodeName, discount);
            techService.CreatePromocode(promocode);
            return View();
        }

        [HttpPost]
        [Route("{promocodeName}/SuccessfulyDeletedPromocode")]
        public IActionResult SuccessfulyDeletedPromocode(string promocodeName)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemovePromocode(promocodeName);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyCreatedUser(string firstName, string lastName, string email, string phoneNumber, string password, string confirmPassword)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            //ApplicationUser user = new ApplicationUser();
            //techService.CreateUser(user);
            return View();
        }

        [HttpPost]
        [Route("{userID}/SuccessfulyDeletedUser")]
        public IActionResult SuccessfulyDeletedUser(string userID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemoveUser(userID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyAddedProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            Product product = new Product(imageURL, categoryName, description, brand, model, price);
            techService.AddProduct(product);
            return View();
        }

        public IActionResult Payment()
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulPayment(string name, string cardNumber, string expiryDate, int cvvNum, string adress)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            Payment payment = new Payment(name, cardNumber, expiryDate, cvvNum, adress);
            techService.AddPayment(payment);
            return View();
        }

        public IActionResult Profile()
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            ApplicationUser user = techService.GetUserByID();
            var viewModel = new ProfileViewModel
            {
                User = user
            };

            return View(viewModel);
        }

        public IActionResult SuccessfulRegister()
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyDeletedProduct(int productID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemoveProduct(productID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyAddedPromotion(decimal newPrice, int productID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.AddPromotion(newPrice, productID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyAddedReview(int productId, int rate, string reviewText)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            techService.AddReview(productId, userId, rate, reviewText);

            return View("SuccessfulyAddedReview");
        }

        [HttpPost]
        public IActionResult SuccessfulyDeletedPromotion(int productId, int promotionID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemovePromotion(productId, promotionID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyRevertedPromotion(int productId, int promotionID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RevertPromotion(productId, promotionID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyCreatedCategory(string categoryName)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            Category category = new Category(categoryName);
            techService.AddCategory(category);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulyDeletedCategory(string categoryName)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.DeleteCategory(categoryName);
            return View();
        }



    }
}
