using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(ApplicationUser user);

        Task<List<ApplicationUser>> GetAllUsersAsync();

        Task<ApplicationUser> GetUserByIDAsync(string userID);

        Task UpdateUserAsync(ApplicationUser user);

        Task DeleteUserAsync(ApplicationUser user);
    }
}
