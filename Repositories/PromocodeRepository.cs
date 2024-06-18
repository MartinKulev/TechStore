using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class PromocodeRepository : BaseRepository<Promocode>, IPromocodeRepository
    {
        private TechStoreDbContext context;

        public PromocodeRepository(TechStoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Promocode> GetPromocodeByIDAsync(int promocodeID)
        {
            return await context.Promocodes.FirstAsync(p => p.PromocodeID == promocodeID);
        }

        public async Task<Promocode> GetPromocodeByPromocodeNameAsync(string promocodeName)
        {
            return await context.Promocodes.FirstOrDefaultAsync(p => p.PromocodeName == promocodeName);
        }
    }
}
