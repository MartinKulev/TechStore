using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace TechStore.Data.ViewModels.ReadInformation
{
    public class CreateUserModel
    {
        public CreateUserModel() { }

        [MaxLength(35)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(35)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(255)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(26)]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(128)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [MaxLength(128)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
