using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels.DisplayInformation
{
    public class ProfileDisplayModel
    {
        public ProfileDisplayModel(ApplicationUser user, List<Order> orders)
        {
            User = user;
            Orders = orders;
        }

        public ApplicationUser User { get; set; }

        public List<Order> Orders { get; set; }
    }
}
