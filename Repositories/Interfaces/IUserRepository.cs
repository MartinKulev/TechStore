using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(ApplicationUser user);

        List<ApplicationUser> GetAllUsers();

        ApplicationUser GetUserByID(string userID);

        void UpdateUser(ApplicationUser user);

        void DeleteUser(ApplicationUser user);
    }
}
