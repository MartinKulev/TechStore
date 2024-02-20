using System.ComponentModel.DataAnnotations;


namespace TechStore.Data.Entities
{
    public class Category
    {
        public Category() { } 

        public Category(int categoryID, string categoryName)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
        }

        [Key]
        public int CategoryID { get; set; }

        [StringLength(100)] 
        public string CategoryName { get; set; }
    }

}
