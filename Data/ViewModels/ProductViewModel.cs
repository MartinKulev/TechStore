using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TechStore.Data.ViewModels.DisplayInformation;
using TechStore.Data.ViewModels.ReadInformation;

namespace TechStore.Data.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel() { }

        public ProductViewModel(ProductDisplayModel productDisplayModel)
        { 
            ProductDisplayModel = productDisplayModel;
            CreateReviewModel = new CreateReviewModel();
        }

        [ValidateNever]
        public ProductDisplayModel ProductDisplayModel { get; set; }
        public CreateReviewModel CreateReviewModel { get; set; }
    }
}
