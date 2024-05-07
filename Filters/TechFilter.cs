﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Services.Interfaces;

namespace TechStore.Filters
{
    public class TechFilter : IResultFilter
    {
        private ICategoryService categoryService;
        private ICartService cartService;
        private IProductService productService;
        private IHttpContextAccessor httpContextAccessor;

        public TechFilter(ICategoryService categoryService, ICartService cartService, IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            this.categoryService = categoryService;
            this.cartService = cartService;
            this.productService = productService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var controller = context.Controller as Controller;
            var httpContext = httpContextAccessor.HttpContext;
            if (controller != null)
            {
                if (controller.TempData["Products"] != null)
                {
                    var productIDs = (controller.TempData["Products"] as IEnumerable<int>).ToList();
                    productIDs = productService.RemoveDisabledProductsIDs(productIDs);
                    controller.TempData["Products"] = productIDs;
                    controller.ViewBag.CartItemsCount = productIDs.Count;
                }
                else
                {
                    controller.ViewBag.CartItemsCount = 0;
                }
                if(httpContext.User.Identity.IsAuthenticated)
                {
                    string userID = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    controller.ViewBag.CartItemsCount = cartService.GetCartItemsCountByUserID(userID);
                }

                List<Category> categories = categoryService.GetAllCategories();
                controller.ViewBag.CategoryList = categories;
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
    }
}