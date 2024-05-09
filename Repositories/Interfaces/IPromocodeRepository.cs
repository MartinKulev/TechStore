using TechStore.Data.Entities;

namespace TechStore.Repositories.Interfaces
{
    public interface IPromocodeRepository
    {
        void CreatePromocode(Promocode promocode);

        List<Promocode> GetAllPromocodes();

        Promocode GetPromocodeByID(int promocodeID);

        Promocode GetPromocodeByPromocodeName(string promocodeName);

        void UpdatePromocode(Promocode promocode);

        void DeletePromocode(Promocode promocode);
    }
}
