using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TechStore.Data.Entities
{
    public class Category
    {
        public Category() { } 

        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }

        [Key]
        [StringLength(100)]
        public string CategoryName { get; set; }
    }

}
