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

        public void CreateUser(string firstName, string lastName, string email, string phoneNumber, string password)
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
            userRepository.CreateUser(user);

            //await userManager.CreateAsync(user, password);
            //await userManager.AddToRoleAsync(user, "User");
        }

        public void DeleteUser(string userID)
        {
            ApplicationUser user = userRepository.GetUserByID(userID);
            userRepository.DeleteUser(user);
        }

        public void EditUser(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            ApplicationUser user = userRepository.GetUserByID(userID);
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
            userRepository.UpdateUser(user);
        }

        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> users = userRepository.GetAllUsers();
            return users;
        }

        public ApplicationUser GetUserByID(string userID)
        {
            ApplicationUser user = userRepository.GetUserByID(userID);
            return user;
        }
    }
}
