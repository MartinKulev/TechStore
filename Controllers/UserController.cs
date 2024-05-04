using Microsoft.AspNetCore.Mvc;
using TechStore.Data.Entities;
using TechStore.Services;

namespace TechStore.Controllers
{
    public class UserController : Controller
    {
        private UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }


        [HttpPost]
        public IActionResult CreatedUser(string firstName, string lastName, string email, string phoneNumber, string password, string confirmPassword)
        {
            var user = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = email,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true
            };

            userService.CreateUser(user);
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
