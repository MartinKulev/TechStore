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

        public void CreateUser(ApplicationUser user)
        {
            context.Add(user);
            context.SaveChanges();
        }
        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> users = context.User.ToList();
            return users;
        }

        public ApplicationUser GetUserByID(string userID)
        {
            ApplicationUser user = context.User.First(p => p.Id == userID);
            return user;
        }

        public void UpdateUser(ApplicationUser user)
        {
            context.Update(user);
            context.SaveChanges();
        }

        public void DeleteUser(ApplicationUser user)
        {
            context.Remove(user);
            context.SaveChanges();
        }
    }
}
