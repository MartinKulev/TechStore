using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechStore.Data.Entities;
using TechStore.Services.Interfaces;

namespace TechStore.Services
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task CreateUserAsync(string firstName, string lastName, string email, string phoneNumber, string password)
        {
            ApplicationUser user = new ApplicationUser(firstName, lastName, email, phoneNumber);
            user.EmailConfirmed = true;

            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "User");
        }

        public async Task DeleteUserAsync(string userID)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userID);
            await userManager.DeleteAsync(user);
        }

        public async Task EditUserAsync(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userID);
            if (newUserName != null)
            {
                user.UserName = newUserName;
            }
            if (newFirstName != null)
            {
                user.FirstName = newFirstName;
            }
            if (newLastName != null)
            {
                user.LastName = newLastName;
            }
            if (newEmail != null)
            {
                user.Email = newEmail;
            }
            if (newPhoneNumber != null)
            {
                user.PhoneNumber = newPhoneNumber;
            }
            await userManager.UpdateAsync(user);
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIDAsync(string userID)
        {
            return await userManager.FindByIdAsync(userID);
        }
    }
}
