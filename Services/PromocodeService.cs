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

        public async Task CreatePromocodeAsync(string promocodeName, decimal discount)
        {
            Promocode promocode = new Promocode(promocodeName, discount);
            await promocodeRepository.CreatePromocodeAsync(promocode);
        }

        public async Task DeletePromocodeAsync(int promocodeID)
        {
            Promocode promocode = await promocodeRepository.GetPromocodeByIDAsync(promocodeID);
            await promocodeRepository.DeletePromocodeAsync(promocode);
        }

        public async Task EditPromocodeAsync(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount)
        {
            Promocode promocode = await promocodeRepository.GetPromocodeByIDAsync(promocodeID);
            if (newPromocodeName != null)
            {
                promocode.PromocodeName = newPromocodeName;
            }
            if (newPromocodeDiscount != 0)
            {
                promocode.Discount = newPromocodeDiscount;
            }
            await promocodeRepository.UpdatePromocodeAsync(promocode);
        }

        public async Task<List<Promocode>> GetAllPromocodesAsync()
        {
            return await promocodeRepository.GetAllPromocodesAsync();
        }

        public async Task<Promocode> ApplyPromocodeAsync(string promocodeName)
        {
            return await promocodeRepository.GetPromocodeByPromocodeNameAsync(promocodeName);
        }
    }
}
