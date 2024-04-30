using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }

        public List<Payment> Payments { get; set; }
    }
}
