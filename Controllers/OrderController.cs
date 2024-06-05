using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.Data.ViewModels;
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
        public async Task<IActionResult> SuccessfulPayment(PaymentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                decimal totalPrice = Convert.ToDecimal(TempData["TotalPrice"]);
                decimal oldTotalPrice = Convert.ToDecimal(TempData["OldTotalPrice"]);
                string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await orderService.CreateOrderAsync(userID, viewModel.PaymentInputModel.Name, viewModel.PaymentInputModel.CardNum, viewModel.PaymentInputModel.ExpiryDate,
                    viewModel.PaymentInputModel.CvvNum, viewModel.PaymentInputModel.Address, totalPrice, oldTotalPrice);
                TempData["Message"] = "The payment was successful!";
                return RedirectToAction("Profile", "Tech");
            }
            else
            {
                return RedirectToAction("Cart", "Tech");
            }
            
        }
    }
}
