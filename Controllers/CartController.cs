﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Data.Entities;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class CartController : Controller
    {
        private CartService cartService;
        private OrderService orderService;
        private ProductService productService;

        public CartController(CartService cartService, OrderService orderService, ProductService productService)
        {
            this.cartService = cartService;        
            this.orderService = orderService;
            this.productService = productService;
        }


        [HttpPost]
        public IActionResult ProductAddedToCart(int productID)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cartService.AddProductToCart(userId, productID);
            }
            else
            {
                List<int> productIDs = new List<int>();
                if (TempData["Products"] != null)
                {
                    productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                }
                productIDs.Add(productID);
                TempData["Products"] = productIDs;
            }
            TempData["Message"] = "Product added to cart.";
            return RedirectToAction("Cart", "Tech");
        }


        [HttpPost]
        public IActionResult ProductRemovedFromCart(int productID)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cartService.RemoveProductFromCart(userId, productID);
            }
            else
            {
                List<int> productIDs = new List<int>();
                if (TempData["Products"] != null)
                {
                    productIDs = (TempData["Products"] as IEnumerable<int>).ToList<int>();
                }
                productIDs.Remove(productID);
                TempData["Products"] = productIDs;
            }
            TempData["Message"] = "Product removed from cart.";
            return RedirectToAction("Cart", "Tech");
        }
    }
}
