using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels.DisplayInformation
{
    public class AdministrationPanelDisplayModel
    {
        public AdministrationPanelDisplayModel(List<Promocode> promocodes, List<ApplicationUser> users, List<Category> categories)
        {
            Promocodes = promocodes;
            Users = users;
            Categories = categories;
        }

        public List<Promocode> Promocodes { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public List<Category> Categories { get; set; }
    }
}
