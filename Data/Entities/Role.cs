using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace TechStore.Data.Entities
{
    public class Role : IdentityRole //no idea how role works
    {
        public string Color { get; set; }
    }
}
