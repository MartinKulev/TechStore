using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TechStore.Data.ViewModels.DisplayInformation;
using TechStore.Data.ViewModels.ReadInformation;

namespace TechStore.Data.ViewModels
{
    public class PaymentViewModel
    {
        public PaymentViewModel()
        { 
            PaymentInputModel = new PaymentInputModel();
        }

        public PaymentInputModel PaymentInputModel { get; set; }
    }
}
