using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace TechStore.Data.Entities
{
    public class Role : IdentityRole //Will add the products manually for now, will add login if there is time left
    {
        public string Color { get; set; }
    }
}
