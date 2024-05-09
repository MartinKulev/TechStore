using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Data.ViewModels;
using TechStore.Services.Interfaces;

namespace TechStore.Controllers
{
    public class TechController : Controller
    {
        private ICartService cartService;
        private ICategoryService categoryService;
        private IOrderService orderService;
        private IProductService productService;
        private IPromocodeService promocodeService;
        private IReviewService reviewService;
        private IUserService userService;

        public TechController(
            ICartService cartService,
            ICategoryService categoryService,
            IOrderService orderService,
            IProductService productService,
            IPromocodeService promocodeService,
            IReviewService reviewService,
            IUserService userService
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
            List<Product> productsInPromotion = productService.GetAllProductsInPromotion();
            return View(productsInPromotion);
        }


        [HttpGet("Tech/Category/{categoryName}")]
        public IActionResult Category(string categoryName)
        {
            List<Product> productsByCategory = productService.GetProductsByCategoryName(categoryName);
            return View(productsByCategory);
        }


        [HttpGet("Tech/Product/{productID}")]
        public IActionResult Product(int productID)
        {
            Product productByID = productService.GetProductByID(productID);
            List<Review> reviewsByProductID = reviewService.GetAllReviewsByProductID(productID);
            var viewModel = new ProductViewModel(reviewsByProductID, productByID);
            return View(viewModel);
        }

        [HttpGet("Tech/Cart/{discount?}")]
        public IActionResult Cart(decimal discount)
        {
            List<Cart> cartItems = new List<Cart>();
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.Identity.IsAuthenticated)
            {
                cartItems = cartService.GetAllCartItemsByUserID(userID);
            }
            else
            {
                List<int> productIDs = new List<int>();
                if (TempData["Products"] != null)
                {
                    productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                }
                cartItems = cartService.GetAllCartItemsByTempData(productIDs, userID);
            }
            List<Product> productsInCart = productService.GetAllProductsInCart(cartItems);
            ViewBag.TotalPrice = cartService.CalculateCartTotalPrice(cartItems, productsInCart);
            if (TempData["PromocodeMessage"] != null)
            {
                if ((string)TempData["PromocodeMessage"] != "Promocode does not exist!")
                {
                    ViewBag.OldTotalPrice = ViewBag.TotalPrice;
                    ViewBag.TotalPrice = Math.Round(ViewBag.OldTotalPrice - (discount / 100 * ViewBag.OldTotalPrice), 2);
                    //TempData["TotalPrice"] = (decimal)ViewBag.TotalPrice;
                }
            }

            var viewModel = new CartViewModel(cartItems, productsInCart);
            return View(viewModel);
        }


        public IActionResult Profile()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser userByID = userService.GetUserByID(userID);
            List<Order> ordersByUserID = orderService.GetAllOrdersByUserID(userID);
            var viewModel = new ProfileViewModel(userByID, ordersByUserID);
            return View(viewModel);
        }


        public IActionResult AdministrationPanel()
        {
            List<Category> categories = categoryService.GetAllCategories();
            List<Promocode> promocodes = promocodeService.GetAllPromocodes();
            List<ApplicationUser> users = userService.GetAllUsers();
            var viewModel = new AdminPanelViewModel(promocodes, users, categories);
            return View(viewModel);
        }

        public IActionResult Payment()
        {
            if (User.Identity.IsAuthenticated)
            {
                //decimal totalPrice = (decimal)TempData["TotalPrice"];
                //TempData["TotalPrice"] = totalPrice;
                return View();
            }
            else
            {
                TempData["You need to log in to continue!"] = "You need to log in to continue!";
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpGet("Tech/Order/{orderID}")]
        public IActionResult Order(string orderID)
        {
            List<Cart> cartItems = cartService.GetAllCartItemsByOrderID(orderID);
            List<Product> products = productService.GetAllProductsInOrder(cartItems);
            Order orderByID = orderService.GetOrderByID(orderID);
            var viewModel = new OrderViewModel(cartItems, products, orderByID);
            return View(viewModel);
        }
    }
}
