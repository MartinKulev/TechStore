using Microsoft.AspNetCore.Identity;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;
using TechStore.Services.Interfaces;

namespace TechStore.Services
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> userManager;
        private IUserRepository userRepository;

        public UserService(UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public async Task CreateUserAsync(string firstName, string lastName, string email, string phoneNumber, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true
            };
            await userRepository.CreateUserAsync(user);

            //await userManager.CreateAsync(user, password);
            //await userManager.AddToRoleAsync(user, "User");
        }

        public async Task DeleteUserAsync(string userID)
        {
            ApplicationUser user = await userRepository.GetUserByIDAsync(userID);
            await userRepository.DeleteUserAsync(user);
        }

        public async Task EditUserAsync(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            ApplicationUser user = await userRepository.GetUserByIDAsync(userID);
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
            await userRepository.UpdateUserAsync(user);
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await userRepository.GetAllUsersAsync();
        }

        public async Task<ApplicationUser> GetUserByIDAsync(string userID)
        {
            return await userRepository.GetUserByIDAsync(userID);
        }
    }
}
