using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IPromocodeRepository : IBaseRepository<Promocode>
    {
        Task<Promocode> GetPromocodeByIDAsync(int promocodeID);

        Task<Promocode> GetPromocodeByPromocodeNameAsync(string promocodeName);
    }
}
