using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TechStore.Data.ViewModels.DisplayInformation;
using TechStore.Data.ViewModels.ReadInformation;

namespace TechStore.Data.ViewModels
{
    public class AdministrationPanelViewModel
    {
        public AdministrationPanelViewModel() { }

        public AdministrationPanelViewModel(AdministrationPanelDisplayModel administrationPanelDisplayModel)
        { 
            AdministrationPanelDisplayModel = administrationPanelDisplayModel;
            CreateProductModel = new CreateProductModel();
            CreateUserModel = new CreateUserModel();
            CreatePromocodeModel = new CreatePromocodeModel();
            CreateCategoryModel = new CreateCategoryModel();
        }

        [ValidateNever]
        public AdministrationPanelDisplayModel AdministrationPanelDisplayModel { get; set; }

        public CreateProductModel CreateProductModel { get; set; }

        public CreateUserModel CreateUserModel { get; set; }

        public CreatePromocodeModel CreatePromocodeModel { get; set; }

        public CreateCategoryModel CreateCategoryModel { get; set; }
    }
}
