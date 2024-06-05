using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TechStore.Data.ViewModels.DisplayInformation;
using TechStore.Data.ViewModels.ReadInformation;

namespace TechStore.Data.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel() { }

        public ProfileViewModel(ProfileDisplayModel profileDisplayModel)
        { 
            ProfileDisplayModel = profileDisplayModel;
        }

        [ValidateNever]
        public ProfileDisplayModel ProfileDisplayModel { get; set; }
    }
}
