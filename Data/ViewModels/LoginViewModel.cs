using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel() { }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
