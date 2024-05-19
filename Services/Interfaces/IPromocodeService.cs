using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IPromocodeService
    {
        Task CreatePromocodeAsync(string promocodeName, decimal discount);

        Task DeletePromocodeAsync(int promocodeID);

        Task EditPromocodeAsync(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount);

        Task<List<Promocode>> GetAllPromocodesAsync();

        Task<Promocode> ApplyPromocodeAsync(string promocodeName);
    }
}
