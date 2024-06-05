using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TechStore.Data.ViewModels.DisplayInformation;
using TechStore.Data.ViewModels.ReadInformation;

namespace TechStore.Data.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel() { }

        public CartViewModel(CartDisplayModel cartDisplayModel)
        {
            CartDisplayModel = cartDisplayModel;
            ApplyPromocodeModel = new ApplyPromocodeModel();
        }

        [ValidateNever]
        public CartDisplayModel CartDisplayModel { get; set; }

        public ApplyPromocodeModel ApplyPromocodeModel { get; set; }
    }
}
