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
            IUserService userService)
        {
            this.cartService = cartService;
            this.categoryService = categoryService;
            this.orderService = orderService;
            this.productService = productService;
            this.promocodeService = promocodeService;
            this.reviewService = reviewService;
            this.userService = userService;
        }


        public async Task<IActionResult> Homepage()
        {
            List<Product> productsInPromotion = await productService.GetAllProductsInPromotionAsync();
            return View(productsInPromotion);
        }


        [HttpGet("Tech/Category/{categoryName}")]
        public async Task<IActionResult> Category(string categoryName)
        {
            List<Product> productsByCategory = await productService.GetProductsByCategoryNameAsync(categoryName);
            return View(productsByCategory);
        }


        [HttpGet("Tech/Product/{productID}")]
        public async Task<IActionResult> Product(int productID)
        {
            Product productByID = await productService.GetProductByIDAsync(productID);
            List<Review> reviewsByProductID = await reviewService.GetAllReviewsByProductIDAsync(productID);
            var viewModel = new ProductViewModel(reviewsByProductID, productByID);
            return View(viewModel);
        }

        public async Task<IActionResult> Cart()
        {
            List<Cart> cartItems = new List<Cart>();
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.Identity.IsAuthenticated)
            {
                cartItems = await cartService.GetAllCartItemsByUserIDAsync(userID);
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
            List<Product> productsInCart = await productService.GetAllProductsInCartAsync(cartItems);

            if (TempData["PromocodeDiscount"] != null && TempData["PromocodeDiscount"].ToString() != "0")
            {
                decimal discount = Convert.ToDecimal(TempData["PromocodeDiscount"]);
                decimal totalPrice = cartService.CalculateCartTotalPrice(cartItems, productsInCart);
                TempData["OldTotalPrice"] = totalPrice.ToString();
                TempData["TotalPrice"] = cartService.ApplyPromocodeToTotalPrice(totalPrice, discount).ToString();
            }
            else
            {
                TempData["TotalPrice"] = cartService.CalculateCartTotalPrice(cartItems, productsInCart);
                TempData["OldTotalPrice"] = "0";
            }

            var viewModel = new CartViewModel(cartItems, productsInCart);
            return View(viewModel);
        }


        public async Task<IActionResult> Profile()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser userByID = await userService.GetUserByIDAsync(userID);
            List<Order> ordersByUserID = await orderService.GetAllOrdersByUserIDAsync(userID);
            var viewModel = new ProfileViewModel(userByID, ordersByUserID);
            return View(viewModel);
        }


        public async Task<IActionResult> AdministrationPanel()
        {
            if (User.IsInRole("Admin"))
            {
                List<Category> categories = await categoryService.GetAllCategoriesAsync();
                List<Promocode> promocodes = await promocodeService.GetAllPromocodesAsync();
                List<ApplicationUser> users = await userService.GetAllUsersAsync();
                var viewModel = new AdminPanelViewModel(promocodes, users, categories);
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Homepage", "Tech");
            }
        }

        public async Task<IActionResult> Payment()
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
        public async Task<IActionResult> Order(string orderID)
        {
            List<Cart> cartItems = await cartService.GetAllCartItemsByOrderIDAsync(orderID);
            List<Product> products = await productService.GetAllProductsInOrderAsync(cartItems);
            Order orderByID = await orderService.GetOrderByIDAsync(orderID);
            var viewModel = new OrderViewModel(cartItems, products, orderByID);
            return View(viewModel);
        }
    }
}
