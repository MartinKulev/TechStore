using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(string firstName, string lastName, string email, string phoneNumber, string password);

        Task DeleteUserAsync(string userID);

        Task EditUserAsync(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber);

        Task<List<ApplicationUser>> GetAllUsersAsync();

        Task<ApplicationUser> GetUserByIDAsync(string id);
    }
}
