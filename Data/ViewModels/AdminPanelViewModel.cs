using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels
{
    public class AdminPanelViewModel
    {
        public List<Promocode> Promocodes { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Category> Categories { get; set; }
    }
}
