using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel() { }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(35)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(35)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(26)]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MaxLength(128)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [MaxLength(128)]
        public required string ConfirmPassword { get; set; }
    }
}
