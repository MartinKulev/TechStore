using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class UserService
    {
        private TechStoreDbContext context;

        public UserService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void CreateUser(ApplicationUser user)
        {
            context.User.Add(user);
            context.SaveChanges();
        }

        public void RemoveUser(string userId)
        {
            ApplicationUser user = context.User.First(p => p.Id == userId);
            context.User.Remove(user);
            context.SaveChanges();
        }

        public void EditUser(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            ApplicationUser oldUser = GetUserByID(userID);
            if (newUserName != null)
            {
                oldUser.UserName = newUserName;
            }
            if (newFirstName != null)
            {
                oldUser.FirstName = newFirstName;
            }
            if (newLastName != null)
            {
                oldUser.LastName = newLastName;
            }
            if (newEmail != null)
            {
                oldUser.Email = newEmail;
            }
            if (newPhoneNumber != null)
            {
                oldUser.PhoneNumber = newPhoneNumber;
            }

            context.Update(oldUser);
            context.SaveChanges();
        }

        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> users = context.User.ToList();
            return users;
        }

        public ApplicationUser GetUserByID(string id)
        {
            ApplicationUser user = context.User.First(p => p.Id == id);
            return user;
        }
    }
}
