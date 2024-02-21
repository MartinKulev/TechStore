using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TechStore.Data.Entities
{
    public class Category
    {
        public Category() { } 

        public Category(int categoryID, string categoryName)
        {
            CategoryName = categoryName;
            CategoryID = categoryID;
        }

        [Key]
        public int CategoryID { get; set; }

        [StringLength(100)] 
        public string CategoryName { get; set; }
    }

}
