using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class PromocodeRepository : IPromocodeRepository
    {
        private TechStoreDbContext context;

        public PromocodeRepository(TechStoreDbContext context)
        {
            this.context = context;
        }

        public async Task CreatePromocodeAsync(Promocode promocode)
        {
            await context.Promocode.AddAsync(promocode);
            await context.SaveChangesAsync();
        }

        public async Task<List<Promocode>> GetAllPromocodesAsync()
        {
            return await context.Promocode.ToListAsync();
        }

        public async Task<Promocode> GetPromocodeByIDAsync(int promocodeID)
        {
            return await context.Promocode.FirstAsync(p => p.PromocodeID == promocodeID);
        }

        public async Task<Promocode> GetPromocodeByPromocodeNameAsync(string promocodeName)
        {
            return await context.Promocode.FirstOrDefaultAsync(p => p.PromocodeName == promocodeName);
        }

        public async Task UpdatePromocodeAsync(Promocode promocode)
        {
            context.Update(promocode);
            await context.SaveChangesAsync();
        }

        public async Task DeletePromocodeAsync(Promocode promocode)
        {
            context.Remove(promocode);
            await context.SaveChangesAsync();
        }
    }
}
