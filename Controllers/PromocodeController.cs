using Microsoft.AspNetCore.Mvc;
using TechStore.Services.Interfaces;
using TechStore.Data.Entities;
using TechStore.Data.ViewModels;

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
        public async Task<IActionResult> CreatedPromocode(string promocodeName, decimal discount)
        {
            await promocodeService.CreatePromocodeAsync(promocodeName, discount);
            TempData["Message"] = "Successfully created a promocode!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public async Task<IActionResult> DeletedPromocode(int promocodeID)
        {
            await promocodeService.DeletePromocodeAsync(promocodeID);
            TempData["Message"] = "Successfully deleted a promocode!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public async Task<IActionResult> EditedPromocode(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount)
        {
            await promocodeService.EditPromocodeAsync(promocodeID, newPromocodeName, newPromocodeDiscount);
            TempData["Message"] = "Successfully edited a promocode!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }

        [HttpPost]
        public async Task<IActionResult> AppliedPromocode(string promocodeName, CartViewModel cartViewModel)
        {
            if (ModelState.IsValid)
            {
                Promocode promocode = await promocodeService.ApplyPromocodeAsync(promocodeName);
                if (promocode == null)
                {
                    TempData["PromocodeMessage"] = $"Promocode does not exist!";
                    TempData["PromocodeDiscount"] = "0";
                }
                else
                {
                    TempData["PromocodeMessage"] = $"{promocode.Discount}% discount with promocode {promocode.PromocodeName}";
                    TempData["PromocodeDiscount"] = promocode.Discount.ToString();
                }
            }           
            return RedirectToAction("Cart", "Tech");
        }
    }
}
