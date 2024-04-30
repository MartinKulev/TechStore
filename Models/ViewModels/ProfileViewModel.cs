using TechStore.Models.Entities;

namespace TechStore.Models.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }

        public List<Payment> Payments { get; set; }
    }
}
