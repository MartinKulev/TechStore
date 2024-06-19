using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Services.Interfaces;
using TechStore.Data.ViewModels;
using TechStore.Data.ViewModels.DisplayInformation;
using TechStore.Data.ViewModels.ReadInformation;
using Microsoft.AspNetCore.Authorization;

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
            var homepageDisplayModel = new HomepageDisplayModel(productsInPromotion);
            var viewModel = new HomepageViewModel(homepageDisplayModel);
            return View(viewModel);
        }


        [HttpGet("Tech/Category/{categoryName}")]
        public async Task<IActionResult> Category(string categoryName)
        {
            List<Product> productsByCategory = await productService.GetProductsByCategoryNameAsync(categoryName);
            var categoryDisplayModel = new CategoryDisplayModel(productsByCategory);
            var viewModel = new CategoryViewModel(categoryDisplayModel);
            return View(viewModel);
        }


        [HttpGet("Tech/Product/{productID}")]
        public async Task<IActionResult> Product(int productID)
        {
            Product productByID = await productService.GetProductByIDAsync(productID);
            List<Review> reviewsByProductID = await reviewService.GetAllReviewsByProductIDAsync(productID);
            var productDisplayModel = new ProductDisplayModel(reviewsByProductID, productByID);
            var viewModel = new ProductViewModel(productDisplayModel);
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

            var cartDisplayModel = new CartDisplayModel(cartItems, productsInCart);
            var viewModel = new CartViewModel(cartDisplayModel);
            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser userByID = await userService.GetUserByIDAsync(userID);
            List<Order> ordersByUserID = await orderService.GetAllOrdersByUserIDAsync(userID);
            var profileDisplayModel = new ProfileDisplayModel(userByID, ordersByUserID);
            var viewModel = new ProfileViewModel(profileDisplayModel);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdministrationPanel()
        {
            List<Category> categories = await categoryService.GetAllCategoriesAsync();
            List<Promocode> promocodes = await promocodeService.GetAllPromocodesAsync();
            List<ApplicationUser> users = await userService.GetAllUsersAsync();
            var administrationPanelDisplayModel = new AdministrationPanelDisplayModel(promocodes, users, categories);
            var viewModel = new AdministrationPanelViewModel(administrationPanelDisplayModel);
            return View(viewModel);
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
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order order = await orderService.GetOrderByIDAsync(orderID);
            if (order.UserID == userID)
            {
                List<Cart> cartItems = await cartService.GetAllCartItemsByOrderIDAsync(orderID);
                List<Product> products = await productService.GetAllProductsInOrderAsync(cartItems);
                var orderDisplayModel = new OrderDisplayModel(cartItems, products, order);
                var viewModel = new OrderViewModel(orderDisplayModel);
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Homepage", "Tech");
            }

        }
    }
}
