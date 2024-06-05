using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TechStore.Controllers.Filters
{
    public class TechFilter : IAsyncResultFilter
    {
        private readonly ICategoryService categoryService;
        private readonly ICartService cartService;
        private readonly IProductService productService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TechFilter(ICategoryService categoryService, ICartService cartService, IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            this.categoryService = categoryService;
            this.cartService = cartService;
            this.productService = productService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var controller = context.Controller as Controller;
            var httpContext = httpContextAccessor.HttpContext;
            if (controller != null)
            {
                if (controller.TempData["Products"] != null)
                {
                    var productIDs = (controller.TempData["Products"] as IEnumerable<int>).ToList();
                    productIDs = await productService.RemoveDisabledProductsIDsAsync(productIDs);
                    controller.TempData["Products"] = productIDs;
                    controller.ViewBag.CartItemsCount = productIDs.Count;
                }
                else
                {
                    controller.ViewBag.CartItemsCount = 0;
                }
                if (httpContext.User.Identity.IsAuthenticated)
                {
                    string userID = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    controller.ViewBag.CartItemsCount = await cartService.GetCartItemsCountByUserIDAsync(userID);
                }

                List<Category> categories = await categoryService.GetAllCategoriesAsync();
                controller.ViewBag.CategoryList = categories;
            }

            await next();
        }
    }
}
