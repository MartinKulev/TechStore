using Microsoft.AspNetCore.Mvc;
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
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
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

        public IActionResult Category(string categoryName)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
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

        public IActionResult ShoppingCart(int cartID)
        {
            List<int> productIDs = new List<int>();
            if (TempData["Products"] != null)
            {
                productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            List<Cart> carts = new List<Cart>();
            List<Product> products = techService.GetAllProducts();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.Identity.IsAuthenticated)
            {
                carts = techService.GetAllCartProductsByUserID(userId);
            }
            else
            {
                foreach (int productID in productIDs)
                {
                    var existingCart = carts.FirstOrDefault(cart => cart.ProductID == productID);
                    if (existingCart != null)
                    {
                        existingCart.Quantity ++;
                    }
                    else
                    {
                        Cart newCart = new Cart(userId, productID, 1);
                        carts.Add(newCart);
                    }
                }
            }
            var viewModel = new CartViewModel
            {
                Carts = carts,
                Products = products
            };

            decimal totalPrice = 0;
            foreach (var cart in carts)
            {
                foreach (var product in products)
                {
                    if (cart.ProductID == product.ProductID)
                    {
                        totalPrice += (product.Price * cart.Quantity);
                    }
                }
            }
            ViewBag.TotalPrice = totalPrice;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Product(int productID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
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
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            List<Promocode> promocodes = techService.GetAllPromocodes();
            List<ApplicationUser> users = techService.GetAllUsers();

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
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            Promocode promocode = new Promocode(promocodeName, discount);
            techService.CreatePromocode(promocode);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyDeletedPromocode(string promocodeName)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemovePromocode(promocodeName);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyDeletedUser(string userID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemoveUser(userID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyAddedProduct(string imageURL, string categoryName, string description, string brand, string model, decimal price)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            Product product = new Product(imageURL, categoryName, description, brand, model, price);
            techService.AddProduct(product);
            return View();
        }


        public IActionResult Payment()
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ShoppingCart");
            }    
        }

        [HttpPost]
        public IActionResult SuccessfulPayment(string name, string cardNumber, string expiryDate, int cvvNum, string adress)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Cart> carts = techService.GetAllCartProductsByUserID(userId);
            TimeSpan eetOffset = TimeSpan.FromHours(3);
            DateTime dateTimeNow = DateTime.UtcNow + eetOffset;
            List<Product> products = techService.GetAllProducts();
            decimal totalPrice = 0;
            foreach (var cart in carts)
            {
                foreach (var product in products)
                {
                    if (cart.ProductID == product.ProductID)
                    {
                        totalPrice += (product.Price * cart.Quantity);
                    }
                }
            }
            Payment payment = new Payment(name, cardNumber, expiryDate, cvvNum, adress, userId, totalPrice, dateTimeNow);
            int paymentID = techService.AddPayment(payment);
            techService.UpdateCartsByUserID(userId, paymentID);
            return View();
        }

        public IActionResult Profile(int orderId)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = techService.GetUserByID(userId);
            List<Payment> payments = techService.GetAllPaymentsByUserID(userId);
            var viewModel = new ProfileViewModel
            {
                User = user,
                Payments = payments
            };
            //var ordersFromDatabase = techService.GetOrders(orderId);// Retrieve orders from your database or another data source


            //var OrderViewModel = ordersFromDatabase.Select(order => new OrderViewModel
            //{
            //    OrderID = order.OrderID,
            //    TotalPrice = order.TotalPrice,
            //    CardNum = order.CardNum,
            //    OrderTime = order.OrderTime
            //}).ToList();

            return View(viewModel);
        }

        public IActionResult SuccessfulRegister()
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyDeletedProduct(int productID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemoveProduct(productID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyAddedPromotion(decimal newPrice, int productID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.AddPromotion(newPrice, productID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyAddedReview(int productId, int rate, string reviewText)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            techService.AddReview(productId, userId, rate, reviewText);

            return View("SuccessfullyAddedReview");
        }

        [HttpPost]
        public IActionResult SuccessfullyDeletedPromotion(int productId, int promotionID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RemovePromotion(productId, promotionID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyRevertedPromotion(int productId, int promotionID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.RevertPromotion(productId, promotionID);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyCreatedCategory(string categoryName)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            Category category = new Category(categoryName);
            techService.AddCategory(category);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyDeletedCategory(string categoryName)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.DeleteCategory(categoryName);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyCreatedUser(string firstName, string lastName, string email, string phoneNumber, string password, string confirmPassword)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
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

        [HttpPost]
        public IActionResult SuccessfullyEditedUser(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.EditUser(userID, newUserName, newFirstName, newLastName, newEmail, newPhoneNumber);

            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyEditedPromocode(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.EditPromocode(promocodeID, newPromocodeName, newPromocodeDiscount);

            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyEditedCategory(int categoryID, string newCategoryName)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            techService.EditCategory(categoryID, newCategoryName);

            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyAddedToCart(int productID)
        {
            List<int> productIDs = new List<int>();
            if (TempData["Products"] != null)
            {
                productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
            }
            else
            {
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;


            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                techService.AddItemToCart(userId, productID);
            }
            else
            {
                productIDs.Add(productID);
            }
            TempData["Products"] = productIDs;
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfullyRemovedProductFromCart(int productID)
        {
            List<int> productIDs = new List<int>();
            if (TempData["Products"] != null)
            {
                productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
            }
            else
            {
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;


            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                techService.RemoveItemFromCart(userId, productID);
            }
            else
            {
                productIDs.Remove(productID);
            }
            TempData["Products"] = productIDs;
            return View();
        }

        public IActionResult Orders(int paymentID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            List<Cart> carts = techService.GetAllCartsByPaymentID(paymentID);
            List<Product> products = techService.GetAllProducts();
            Payment payment = techService.GetPaymentByID(paymentID);
            var viewModel = new OrdersViewModel
            {
                Carts = carts,
                Products = products,
                Payment = payment
            };
            return View(viewModel);
        }

        public IActionResult VerificationLinkWasSentToYourEmail(int paymentID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = techService.GetAllCategories();
            ViewBag.ItemsList = categories;

            return View();
        }
    }
}
