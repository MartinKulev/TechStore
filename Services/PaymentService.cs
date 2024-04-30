using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class PaymentService
    {
        private TechStoreDbContext context;

        public PaymentService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public int AddPayment(Payment payment)//
        {
            context.Payment.Add(payment);
            context.SaveChanges();

            context.Entry(payment).Reload();
            int generatedKey = payment.PaymentID;
            return generatedKey;
        }

        public List<Payment> GetAllPaymentsByUserID(string usedID)
        {
            List<Payment> payments = context.Payment.Where(p => p.UserID == usedID).ToList();
            return payments;
        }

        public Payment GetPaymentByID(int paymentID)
        {
            Payment payment = context.Payment.First(p => p.PaymentID == paymentID);
            return payment;
        }

    }
}
