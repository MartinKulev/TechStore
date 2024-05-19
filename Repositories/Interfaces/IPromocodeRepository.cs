using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IPromocodeRepository
    {
        Task CreatePromocodeAsync(Promocode promocode);

        Task<List<Promocode>> GetAllPromocodesAsync();

        Task<Promocode> GetPromocodeByIDAsync(int promocodeID);

        Task<Promocode> GetPromocodeByPromocodeNameAsync(string promocodeName);

        Task UpdatePromocodeAsync(Promocode promocode);

        Task DeletePromocodeAsync(Promocode promocode);
    }
}
