using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IUserService
    {
        void CreateUser(string firstName, string lastName, string email, string phoneNumber, string password);

        void DeleteUser(string userId);

        void EditUser(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber);

        List<ApplicationUser> GetAllUsers();

        ApplicationUser GetUserByID(string id);
    }
}
