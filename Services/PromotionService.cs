using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class PromotionService
    {
        private TechStoreDbContext context;
        private ProductService productService;

        public PromotionService(TechStoreDbContext context, ProductService productService)
        {
            this.context = context;
            this.productService = productService;
        }

        public void CreatePromotion(decimal newPrice, int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            product.IsInPromotion = true;
            context.Update(product);
            Promotion promotion = new Promotion(productID, newPrice);
            context.Promotion.Add(promotion);
            context.SaveChanges();
        }

        public void RemovePromotion(int productID, int promotionID)
        {
            productService.RemoveProduct(productID);//
            Promotion promotion = context.Promotion.First(p => p.PromotionID == promotionID);
            context.Promotion.Remove(promotion);
            context.SaveChanges();
        }

        public void RevertPromotion(int productID, int promotionID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            product.IsInPromotion = false;
            context.Update(product);
            Promotion promotion = context.Promotion.First(p => p.PromotionID == promotionID);
            context.Promotion.Remove(promotion);
            context.SaveChanges();
        }

        public List<Promotion> GetAllPromotions()
        {
            List<Promotion> promotions = context.Promotion.OrderBy(p => p.NewPrice).ToList();
            return promotions;
        }
    }
}
