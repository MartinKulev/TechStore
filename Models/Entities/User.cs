using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models.Entities
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}

