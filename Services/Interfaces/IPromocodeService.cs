using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IPromocodeService
    {
        void CreatePromocode(string promocodeName, decimal discount);

        void DeletePromocode(string promocodeName);

        void EditPromocode(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount);

        List<Promocode> GetAllPromocodes();

        Promocode GetPromocodeByID(int promocodeID);
    }
}
