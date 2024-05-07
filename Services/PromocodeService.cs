using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Services.Interfaces;

namespace TechStore.Services
{
    public class PromocodeService : IPromocodeService
    {
        private TechStoreDbContext context;

        public PromocodeService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void CreatePromocode(string promocodeName, decimal discount)
        {
            Promocode promocode = new Promocode(promocodeName, discount);
            context.Promocode.Add(promocode);
            context.SaveChanges();
        }

        public void DeletePromocode(string promocodeName)
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
            Promocode promocode = context.Promocode.First(p => p.PromocodeID == promocodeID);
            return promocode;
        }
    }
}
