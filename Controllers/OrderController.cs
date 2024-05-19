using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> SuccessfulPayment(string name, string cardNumber, string expiryDate, int cvvNum, string adress)
        {
            decimal totalPrice = Convert.ToDecimal(TempData["TotalPrice"]);
            decimal oldTotalPrice = Convert.ToDecimal(TempData["OldTotalPrice"]);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await orderService.CreateOrderAsync(userID, name, cardNumber, expiryDate, cvvNum, adress, totalPrice, oldTotalPrice);
            TempData["Message"] = "The payment was successful!";
            return RedirectToAction("Profile", "Tech");
        }
    }
}
