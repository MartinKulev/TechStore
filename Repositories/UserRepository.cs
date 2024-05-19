using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private TechStoreDbContext context;

        public UserRepository(TechStoreDbContext context)
        {
            this.context = context;
        }

        public async Task CreateUserAsync(ApplicationUser user)
        {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await context.User.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIDAsync(string userID)
        {
            return await context.User.FirstAsync(p => p.Id == userID);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            context.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            context.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}
