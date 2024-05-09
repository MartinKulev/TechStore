﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Services.Interfaces;

namespace TechStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public IActionResult SuccessfulPayment(string name, string cardNumber, string expiryDate, int cvvNum, string adress)
        {
            //decimal totalPrice = (decimal)TempData["TotalPrice"];
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            orderService.CreateOrder(userID, name, cardNumber, expiryDate, cvvNum, adress);
            TempData["Message"] = "The payment was successful!";
            return RedirectToAction("Profile", "Tech");
        }
    }
}
