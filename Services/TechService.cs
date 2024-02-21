using Microsoft.AspNetCore.Authentication;
using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class TechService
    {
        private TechStoreContext context;

        public TechService(TechStoreContext context)
        {
            this.context = context;
        }

        public void AddCategory(Category category)
        {
            if (!context.Category.Contains(category))
            {
                context.Category.Add(category);
                context.SaveChanges();
            }
        }
    }
}
