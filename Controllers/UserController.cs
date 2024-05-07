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
        public IActionResult CreatedUser(string firstName, string lastName, string email, string phoneNumber, string password, string confirmPassword)
        {
            userService.CreateUser(firstName, lastName, email, phoneNumber, password);
            TempData["Message"] = "Successfully created a user!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public IActionResult DeletedUser(string userID)
        {
            userService.DeleteUser(userID);
            TempData["Message"] = "Successfully edited a user!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }


        [HttpPost]
        public IActionResult EditedUser(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            userService.EditUser(userID, newUserName, newFirstName, newLastName, newEmail, newPhoneNumber);
            TempData["Message"] = "Successfully edited a user!";
            return RedirectToAction("AdministrationPanel", "Tech");
        }
    }
}
