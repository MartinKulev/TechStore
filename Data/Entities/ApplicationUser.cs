using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser() { }

        //public ApplicationUser(string firstName, string lastName, string email, string phoneNumber)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    UserName = email;
        //    Email = email;
        //    PhoneNumber = phoneNumber;
        //    EmailConfirmed = true;
        //}

        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }
    }
}

