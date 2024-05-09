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
        public void CreatePromocode(Promocode promocode)
        {
            context.Promocode.Add(promocode);
            context.SaveChanges();
        }

        public List<Promocode> GetAllPromocodes()
        {
            List<Promocode> promocodes = context.Promocode.ToList();
            return promocodes;
        }

        public Promocode GetPromocodeByID(int promocodeID)
        {
            Promocode promocode = context.Promocode.First(p => p.PromocodeID == promocodeID);
            return promocode;
        }

        public Promocode GetPromocodeByPromocodeName(string promocodeName)
        {
            Promocode promocode = context.Promocode.FirstOrDefault(p => p.PromocodeName == promocodeName);
            return promocode;
        }

        public void UpdatePromocode(Promocode promocode)
        {
            context.Update(promocode);
            context.SaveChanges();
        }

        public void DeletePromocode(Promocode promocode)
        {
            context.Remove(promocode);
            context.SaveChanges();
        }
    }
}
