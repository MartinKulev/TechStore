using TechStore.Data.Entities;

namespace TechStore.Services.Interfaces
{
    public interface IPromocodeService
    {
        void CreatePromocode(string promocodeName, decimal discount);

        void DeletePromocode(int promocodeID);

        void EditPromocode(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount);

        List<Promocode> GetAllPromocodes();
    }
}
