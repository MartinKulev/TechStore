namespace TechStore.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Required]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [Phone]
    public required string PhoneNumber { get; set; }

    [Required]
    public required string Gender { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required]
    [PasswordConfirmation]
    public required string PasswordConfirmation { get; set; }

    private class PasswordConfirmationAttribute : Attribute
    {
        public PasswordConfirmationAttribute() { }  
        public PasswordConfirmationAttribute(string confirmation) { }
    }
}