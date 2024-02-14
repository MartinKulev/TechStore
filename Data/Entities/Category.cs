using System.ComponentModel.DataAnnotations;


namespace TechStore.Data.Entities
{
    public class Category
    {
        public Category() { } //Why do we even need a Category table?

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

        public string CategoryImageUrl { get; set; } //We need imageUrl for every individual product, not for every category?

        public bool IsActive { get; set; }



    }

}
