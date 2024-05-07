using Microsoft.AspNetCore.Identity;


namespace TechStore.Data.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
