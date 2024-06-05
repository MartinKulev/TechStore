using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TechStore.Data.ViewModels.DisplayInformation;
using TechStore.Data.ViewModels.ReadInformation;

namespace TechStore.Data.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel() { }

        public OrderViewModel(OrderDisplayModel orderDisplayModel)
        { 
            OrderDisplayModel = orderDisplayModel;
        }

        [ValidateNever]
        public OrderDisplayModel OrderDisplayModel { get; set; }
    }
}
