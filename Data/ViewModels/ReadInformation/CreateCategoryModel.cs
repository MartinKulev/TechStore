using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.ViewModels.ReadInformation
{
    public class CreateCategoryModel
    {
        public CreateCategoryModel() { }
            
        [MaxLength(13)]
        public string CategoryName { get; set; }
    }
}
