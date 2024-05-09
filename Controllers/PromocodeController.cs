using Microsoft.AspNetCore.Mvc;
using TechStore.Services.Interfaces;
using TechStore.Data.Entities;

namespace TechStore.Controllers
{
    public class PromocodeController : Controller
    {
        private IPromocodeService promocodeService;

        public PromocodeController(IPromocodeService promocodeService)
        {
            this.promocodeService = promocodeService;
        }


        [HttpPost]
        public IActionResult CreatedPromocode(string promocodeName, decimal discount)
        {
            promocodeService.CreatePromocode(promocodeName, discount);
            TempData["Message"] = "Successfully created a promocode!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public IActionResult DeletedPromocode(int promocodeID)
        {
            promocodeService.DeletePromocode(promocodeID);
            TempData["Message"] = "Successfully deleted a promocode!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public IActionResult EditedPromocode(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount)
        {
            promocodeService.EditPromocode(promocodeID, newPromocodeName, newPromocodeDiscount);
            TempData["Message"] = "Successfully edited a promocode!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }

        [HttpPost]
        public IActionResult AppliedPromocode(string promocodeName)
        {
            Promocode promocode = promocodeService.ApplyPromocode(promocodeName);
            decimal discount = 0;
            if (promocode == null)
            {
                TempData["PromocodeMessage"] = $"Promocode does not exist!";
            }
            else
            {
                discount = promocode.Discount;
                TempData["PromocodeMessage"] = $"{promocode.Discount}% discount with promocode {promocode.PromocodeName}";
            }
            return RedirectToAction("Cart", "Tech", new { discount = discount });
        }
    }
}
