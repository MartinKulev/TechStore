using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Data.ViewModels;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class TechController : Controller
    {
        private CartService cartService;
        private CategoryService categoryService;
        private OrderService orderService;
        private ProductService productService;
        private PromocodeService promocodeService;
        private ReviewService reviewService;
        private UserService userService;

        public TechController(
            CartService cartService,
            CategoryService categoryService,
            OrderService orderService,
            ProductService productService,
            PromocodeService promocodeService,
            ReviewService reviewService,
            UserService userService
            )
        {
            this.cartService = cartService;
            this.categoryService = categoryService;
            this.orderService = orderService;
            this.productService = productService;
            this.promocodeService = promocodeService;
            this.reviewService = reviewService;
            this.userService = userService;
        }

        public IActionResult Homepage()
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            List<Product> productsInPromotion = productService.GetAllProductsInPromotion();
            return View(productsInPromotion);
        }


        [HttpGet("Tech/Category/{categoryName}")]
        public IActionResult Category(string categoryName)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            List<Product> productsByCategory = productService.GetProductsByCategory(categoryName);
            return View(productsByCategory);
        }

        public IActionResult ShoppingCart(int cartID)
        {
            List<int> productIDs = new List<int>();
            if (TempData["Products"] != null)
            {
                productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            List<Cart> carts = new List<Cart>();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.Identity.IsAuthenticated)
            {
                carts = cartService.GetAllCartProductsByUserID(userId);
            }
            else
            {
                carts = cartService.GetAllCartProductsByTempData(productIDs, userId);
            }
            List<Product> productsInCart = productService.GetAllProductsInCart(carts);           
            ViewBag.TotalPrice = cartService.CalculateCartTotalPrice(carts, productsInCart);
            var viewModel = new CartViewModel(carts, productsInCart);
            return View(viewModel);
        }

        [HttpGet("Tech/Product/{productID}")]
        public IActionResult Product(int productID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            Product product = productService.GetProductByID(productID);
            List<Review> reviews = reviewService.GetAllReviews(productID);
            List<ApplicationUser> users = userService.GetAllUsers();

            var viewModel = new ProductViewModel(reviews, product, users);
            return View(viewModel);
        }

        public IActionResult AdministrationPanel()
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            List<Promocode> promocodes = promocodeService.GetAllPromocodes();
            List<ApplicationUser> users = userService.GetAllUsers();

            var viewModel = new AdminPanelViewModel(promocodes, users, categories);

            return View(viewModel);
        }

        public IActionResult Payment()
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ShoppingCart");
            }
        }

        public IActionResult Profile(int orderId)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = userService.GetUserByID(userId);
            List<Order> orders = orderService.GetAllOrdersByUserID(userId);
            var viewModel = new ProfileViewModel(user, orders);
            return View(viewModel);
        }

        [HttpGet("Tech/Order/{orderID}")]
        public IActionResult Order(int orderID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            List<Cart> carts = cartService.GetAllCartsByOrderID(orderID);
            List<Product> products = productService.GetAllProducts();
            Order order = orderService.GetOrderByID(orderID);
            var viewModel = new OrderViewModel(carts, products, order);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            Promocode promocode = new Promocode(promocodeName, discount);
            promocodeService.CreatePromocode(promocode);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            promocodeService.DeletePromocode(promocodeName);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            userService.DeleteUser(userID);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            Product product = new Product(imageURL, categoryName, description, brand, model, price);
            productService.CreateProduct(product);
            return View();
        }

        [HttpPost]
        public IActionResult SuccessfulPayment(string name, string cardNumber, string expiryDate, int cvvNum, string adress)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Cart> carts = cartService.GetAllCartProductsByUserID(userId);
            TimeSpan eetOffset = TimeSpan.FromHours(3);
            DateTime dateTimeNow = DateTime.UtcNow + eetOffset;
            List<Product> products = productService.GetAllProducts();
            decimal totalPrice = 0;
            foreach (var cart in carts)
            {
                foreach (var product in products)
                {
                    if (cart.ProductID == product.ProductID)
                    {
                        if (product.IsInPromotion)
                        {
                            totalPrice += (product.NewPrice * cart.Quantity);
                        }
                        else
                        {
                            totalPrice += (product.Price * cart.Quantity);
                        }

                    }
                }
            }
            Order order = new Order(name, cardNumber, expiryDate, cvvNum, adress, userId, totalPrice, dateTimeNow);
            int orderID = orderService.AddOrder(order);
            cartService.UpdateCartsByUserID(userId, orderID);
            return View();
        }

        public IActionResult SuccessfulRegister()
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            productService.RemoveProduct(productID);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            productService.AddProductToPromotion(newPrice, productID);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            reviewService.CreateReview(productId, userId, rate, reviewText);

            return View("SuccessfullyAddedReview");
        }

        //[HttpPost]
        //public IActionResult SuccessfullyDeletedPromotion(int productId, int promotionID)
        //{
        //    if (TempData["Products"] != null)
        //    {
        //        var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
        //        TempData["Products"] = productIDs;
        //    }
        //    List<Category> categories = categoryService.GetAllCategories();
        //    ViewBag.CategoryList = categories;

        //    promotionService.RemovePromotion(productId, promotionID);
        //    return View();
        //}

        [HttpPost]
        public IActionResult SuccessfullyRevertedPromotion(int productId)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            productService.RevertPromotion(productId);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            Category category = new Category(categoryName);
            categoryService.CreateCategory(category);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            categoryService.DeleteCategory(categoryName);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            var user = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = email,
                Email = email,
                PhoneNumber = phoneNumber
            };

            userService.CreateUser(user);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            userService.EditUser(userID, newUserName, newFirstName, newLastName, newEmail, newPhoneNumber);

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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            promocodeService.EditPromocode(promocodeID, newPromocodeName, newPromocodeDiscount);

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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            categoryService.EditCategory(categoryID, newCategoryName);

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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;


            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cartService.AddItemToCart(userId, productID);
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
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;


            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cartService.RemoveItemFromCart(userId, productID);
            }
            else
            {
                productIDs.Remove(productID);
            }
            TempData["Products"] = productIDs;
            return View();
        }

        public IActionResult VerificationLinkWasSentToYourEmail(int orderID)
        {
            if (TempData["Products"] != null)
            {
                var productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                TempData["Products"] = productIDs;
            }
            List<Category> categories = categoryService.GetAllCategories();
            ViewBag.CategoryList = categories;

            return View();
        }
    }
}
