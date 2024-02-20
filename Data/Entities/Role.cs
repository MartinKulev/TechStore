using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace TechStore.Data.Entities
{
    public class Role : IdentityRole 
    {
        public string Color { get; set; }
    }
}
