using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace TechStore.Data.Entities
{
    public class User : IdentityUser 
    {
        [StringLength(50)] 
        public string Name { get; set; }  

        public string Address { get; set; }

        [StringLength(50)]
        public string PostCode { get; set; }

        public string ImageUrl { get; set; }    
    
    }
}