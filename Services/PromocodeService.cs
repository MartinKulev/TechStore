using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;
using TechStore.Services.Interfaces;

namespace TechStore.Services
{
    public class PromocodeService : IPromocodeService
    {
        private IPromocodeRepository promocodeRepository;

        public PromocodeService(IPromocodeRepository promocodeRepository)
        {
            this.promocodeRepository = promocodeRepository;
        }

        public void CreatePromocode(string promocodeName, decimal discount)
        {
            Promocode promocode = new Promocode(promocodeName, discount);
            promocodeRepository.CreatePromocode(promocode);
        }

        public void DeletePromocode(int promocodeID)
        {
            Promocode promocode = promocodeRepository.GetPromocodeByID(promocodeID);
            promocodeRepository.DeletePromocode(promocode);
        }

        public void EditPromocode(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount)
        {
            Promocode promocode = promocodeRepository.GetPromocodeByID(promocodeID);
            if (newPromocodeName != null)
            {
                promocode.PromocodeName = newPromocodeName;
            }
            if (newPromocodeDiscount != 0)
            {
                promocode.Discount = newPromocodeDiscount;
            }
            promocodeRepository.UpdatePromocode(promocode);
        }

        public List<Promocode> GetAllPromocodes()
        {
            List<Promocode> promocode = promocodeRepository.GetAllPromocodes();
            return promocode;
        }
    }
}
