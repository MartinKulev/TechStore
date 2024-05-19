using Microsoft.AspNetCore.Mvc;
using TechStore.Services.Interfaces;

namespace TechStore.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> CreatedUser(string firstName, string lastName, string email, string phoneNumber, string password, string confirmPassword)
        {
            await userService.CreateUserAsync(firstName, lastName, email, phoneNumber, password);
            TempData["Message"] = "Successfully created a user!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public async Task<IActionResult> DeletedUser(string userID)
        {
            await userService.DeleteUserAsync(userID);
            TempData["Message"] = "Successfully edited a user!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public async Task<IActionResult> EditedUser(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            await userService.EditUserAsync(userID, newUserName, newFirstName, newLastName, newEmail, newPhoneNumber);
            TempData["Message"] = "Successfully edited a user!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }
    }
}
