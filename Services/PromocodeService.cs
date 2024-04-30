using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class PromocodeService
    {
        private TechStoreDbContext context;

        public PromocodeService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void CreatePromocode(Promocode promocode)
        {
            context.Promocode.Add(promocode);
            context.SaveChanges();
        }

        public void RemovePromocode(string promocodeName)
        {
            Promocode promocode = context.Promocode.First(p => p.PromocodeName == promocodeName);
            context.Promocode.Remove(promocode);
            context.SaveChanges();
        }

        public void EditPromocode(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount)
        {
            Promocode promocode = GetPromocodeByID(promocodeID);
            if (newPromocodeName != null)
            {
                promocode.PromocodeName = newPromocodeName;
            }
            if (newPromocodeDiscount != 0)
            {
                promocode.Discount = newPromocodeDiscount;
            }

            context.Update(promocode);
            context.SaveChanges();
        }

        public List<Promocode> GetAllPromocodes()
        {
            List<Promocode> promocodes = context.Promocode.ToList();
            return promocodes;
        }

        public Promocode GetPromocodeByID(int promocodeID)
        {
            Promocode promocode = context.Promocode.First(p => p.PromocodeId == promocodeID);
            return promocode;
        }
    }
}
