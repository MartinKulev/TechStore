using System.ComponentModel.DataAnnotations;


namespace TechStore.Data.Entities
{
    public class Category
    {
        public Category() { }

        public Category(int categoryId, string categoryName, string categoryImageUrl, bool isActive)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryImageUrl = categoryImageUrl;
            IsActive = isActive;
        }
        [Key]
        public int CategoryId { get; set; }

        [StringLength(100)] 
        public string CategoryName { get; set; }

        public string CategoryImageUrl { get; set; }

        public bool IsActive { get; set; }



    }

}
