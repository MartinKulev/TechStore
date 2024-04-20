﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
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
            //techService.AddАrtificiallyCategories(); //This method exists for developing purposes only. Uncomment and run it once to add categories.
            
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
            List<Promotion> promotions = techService.GetAllPromotions();

            var viewModel = new HomepageViewModel
            {
                Promotions = promotions,
                Products = products
            };

            return View(viewModel);
        }

        public IActionResult ShoppingCart(string cartID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            var cartItems = techService.GetCartItems(cartID);


            var cartViewModels = cartItems.Select(item => new CartViewModel
            {
                ImageURL = item.ImageURL,
                Description = item.Description,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList();

            return View(cartViewModels);
        }

        [HttpPost]
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
        public IActionResult SuccessfullyCreatedPromocode(string promocodeName, decimal discount)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            Promocode promocode = new Promocode(promocodeName, discount);
            techService.CreatePromocode(promocode);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyDeletedPromocode(string promocodeName)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemovePromocode(promocodeName);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyDeletedUser(string userID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemoveUser(userID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyAddedProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price)
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

        public IActionResult Profile(int orderId)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = techService.GetUserByID(userId);
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
        public IActionResult SuccessfullyDeletedProduct(int productID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemoveProduct(productID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyAddedPromotion(decimal newPrice, int productID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.AddPromotion(newPrice, productID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyAddedReview(int productId, int rate, string reviewText)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            techService.AddReview(productId, userId, rate, reviewText);

            return View("SuccessfullyAddedReview");
        }

        [HttpPost]
        public IActionResult SuccessfullyDeletedPromotion(int productId, int promotionID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemovePromotion(productId, promotionID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyRevertedPromotion(int productId, int promotionID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RevertPromotion(productId, promotionID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyCreatedCategory(string categoryName)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            Category category = new Category(categoryName);
            techService.AddCategory(category);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyDeletedCategory(string categoryName)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.DeleteCategory(categoryName);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyCreatedUser(string firstName, string lastName, string email, string phoneNumber, string password, string confirmPassword)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            var user = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = email,
                Email = email,
                PhoneNumber = phoneNumber
            };

            techService.CreateUser(user);
            return View("SuccessfullyCreatedUser");
        }

        public IActionResult Index()
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            var products = techService.GetAllProducts();

            return View(products);
        }

        [HttpPost]
        public IActionResult AddToCart(string userId, int productId, int quantity, decimal price, string description, string imageURL)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            try
            {

                bool success = techService.AddProductToCart(userId, productId, quantity, price, description, imageURL);

                if (success)
                {

                    return RedirectToAction("ShoppingCart", "Tech");
                }
                else
                {

                    ModelState.AddModelError("", "Failed to add the product to the cart.");
                    return RedirectToAction("Mice", "Tech");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"An error occurred while adding the product to the cart: {ex.Message}");
                return RedirectToAction("Laptops", "Tech");
            }
        }


        [HttpPost]
        public IActionResult ApplyPromoCode(string promoCode)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

           string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            decimal totalPrice = techService.GetTotalCartPrice(userId);

            ViewBag.TotalPrice = totalPrice;
            var promo = techService.GetPromoCode(promoCode);
            if (promo != null)
            {
                decimal discountAmount = totalPrice * (promo.Discount / 100);
                decimal discountedPrice = totalPrice - discountAmount;


                ViewBag.TotalPrice = discountedPrice;


                ViewBag.DiscountMessage = $"Отстъпка: {promo.Discount}%";
            }
            else
            {

                ViewBag.TotalPrice = totalPrice;


                ViewBag.DiscountMessage = "Грешен код";
            }

            return PartialView("DiscountMessagePartial");
        }



        [HttpPost]
        public IActionResult AddToTempOrder()
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var cartItems = techService.GetCartItems(userId);


            foreach (var item in cartItems)
            {
                var tempOrder = new TempOrder
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UserID = userId,
                    OrderTime = DateTime.Now
                };

                techService.AddToTempOrder(tempOrder);
            }


            return View("Payment");
        }

        [HttpPost]
        public IActionResult ProcessPayment(string name, int cardNumber, string expiryDate, string cvvNum, string adress)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var tempOrders = techService.GetTempOrders(userId);


            foreach (var tempOrder in tempOrders)
            {
                var order = new TechStore.Models.Entities.Order
                {
                    ProductID = tempOrder.ProductID,
                    Quantity = tempOrder.Quantity,
                    UserID = userId,
                    CardNum = cardNumber,
                    OrderTime = tempOrder.OrderTime
                };

                techService.AddOrder(order);
            }


            techService.ClearTempOrders(userId);

            techService.DeleteCartItems(userId);

            return View("Profile");
        }

        private int GetUserID()
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim != null)
                {
                    if (int.TryParse(userIdClaim.Value, out int userId))
                    {
                        return userId;
                    }
                }
            }

            return -1;
        }

        [HttpPost]
        public IActionResult SuccessfullyEditedUser(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.EditUser(userID, newUserName, newFirstName, newLastName, newEmail, newPhoneNumber);

            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyEditedPromocode(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.EditPromocode(promocodeID, newPromocodeName, newPromocodeDiscount);

            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyEditedCategory(int categoryID, string newCategoryName)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.EditCategory(categoryID, newCategoryName);

            return View();
        }

        [HttpPost]
        public ActionResult Orders(int orderID)
        {
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            var orders = techService.GetOrders();


            var orderViewModels = orders.Select(order => new OrderViewModel
            {

                TotalPrice = order.TotalPrice,
                CardNum = order.CardNum,
                OrderTime = order.OrderTime

            }).ToList();


            return View(orderViewModels);
        }
    }
}
