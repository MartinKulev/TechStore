using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TechStore.Data.ViewModels.DisplayInformation;
using TechStore.Data.ViewModels.ReadInformation;

namespace TechStore.Data.ViewModels
{
    public class HomepageViewModel
    {
        public HomepageViewModel() { }

        public HomepageViewModel(HomepageDisplayModel homepageDisplayModel)
        { 
            HomepageDisplayModel = homepageDisplayModel;
        }

        [ValidateNever]
        public HomepageDisplayModel HomepageDisplayModel { get; set; }
    }
}
