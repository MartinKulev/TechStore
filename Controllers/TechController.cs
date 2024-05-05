using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            List<Product> productsInPromotion = productService.GetAllProductsInPromotion();
            return View(productsInPromotion);
        }


        [HttpGet("Tech/Category/{categoryName}")]
        public IActionResult Category(string categoryName)
        {
            List<Product> productsByCategory = productService.GetProductsByCategory(categoryName);
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


        public IActionResult Cart(int cartID)
        {
            List<Cart> cartItems = new List<Cart>();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.Identity.IsAuthenticated)
            {
                cartItems = cartService.GetAllCartItemsByUserID(userId);
            }
            else
            {
                List<int> productIDs = new List<int>();
                if (TempData["Products"] != null)
                {
                    productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                }
                cartItems = cartService.GetAllCartItemsByTempData(productIDs, userId);
            }
            List<Product> productsInCart = productService.GetAllProductsInCart(cartItems);           
            ViewBag.TotalPrice = cartService.CalculateCartTotalPrice(cartItems, productsInCart);
            var viewModel = new CartViewModel(cartItems, productsInCart);
            return View(viewModel);
        }


        public IActionResult Profile()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser userByID = userService.GetUserByID(userId);
            List<Order> ordersByUserID = orderService.GetAllOrdersByUserID(userId);
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
            List<Product> products = productService.GetAllProducts();
            Order orderByID = orderService.GetOrderByID(orderID);
            var viewModel = new OrderViewModel(cartItems, products, orderByID);
            return View(viewModel);
        }
    }
}
