using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TechStore.Data.ViewModels.DisplayInformation;

namespace TechStore.Data.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel() { }

        public CategoryViewModel(CategoryDisplayModel categoryDisplayModel)
        {
            CategoryDisplayModel = categoryDisplayModel;
        }

        [ValidateNever]
        public CategoryDisplayModel CategoryDisplayModel { get; set; }
    }
}
