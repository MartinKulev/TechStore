using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TechStore.Models.Entities
{
    public class Category
    {
        public Category() { }

        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }

        [Key]
        public string CategoryName { get; set; }
    }

}
