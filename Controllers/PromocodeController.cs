using Microsoft.AspNetCore.Mvc;
using TechStore.Data.Entities;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class PromocodeController : Controller
    {
        private PromocodeService promocodeService;

        public PromocodeController(PromocodeService promocodeService)
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
        public IActionResult DeletedPromocode(string promocodeName)
        {
            promocodeService.DeletePromocode(promocodeName);
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
    }
}
