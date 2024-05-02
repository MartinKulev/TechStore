using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel(ApplicationUser user, List<Order> orders)
        {
            User = user;
            Orders = orders;
        }

        public ApplicationUser User { get; set; }

        public List<Order> Orders { get; set; }
    }
}
