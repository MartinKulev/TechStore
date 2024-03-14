
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechStore.Data;
using TechStore.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TechStore.Services
{
    public class TechService
    {
        private TechStoreDbContext context;

        public TechService(TechStoreDbContext context)
        {
            this.context = context;
        }

        public void AddАrtificiallyCategories()//this method exists for developing purposes only
        {
            Category category1 = new Category("Laptops");
            Category category2 = new Category("Smartphones");
            Category category3 = new Category("Tablets");
            Category category4 = new Category("TVs");
            Category category5 = new Category("Displays");
            Category category6 = new Category("Keyboards");
            Category category7 = new Category("Mice");
            Category category8 = new Category("Headphones");
            Category category9 = new Category("Speakers");
            AddCategory(category1);
            AddCategory(category2);
            AddCategory(category3);
            AddCategory(category4);
            AddCategory(category5);
            AddCategory(category6);
            AddCategory(category7);
            AddCategory(category8);
            AddCategory(category9);
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = context.Category.ToList();
            return categories;
        }

        public void AddCategory(Category category)
        {
            if (!context.Category.Contains(category))
            {
                context.Category.Add(category);
                context.SaveChanges();
            }
        }

        public void DeleteCategory(string categoryName)
        {
            Category category = context.Category.First(p => p.CategoryName == categoryName);
            context.Category.Remove(category);
            DeleteAllProductsFromACategory(categoryName);
            context.SaveChanges();
        }

        public void DeleteAllProductsFromACategory(string categoryName)
        {
            List<Product> products = context.Product.Where(p => p.CategoryName == categoryName).ToList();
            foreach (var product in products)
            {
                context.Product.Remove(product);
            }
            context.SaveChanges();
        }

        public void AddProduct(Product product)
        {
            context.Product.Add(product);
            context.SaveChanges();
        }

        public List<Product> GetProductsByCategories(string category)
        {
            if (context.Product.Any(p => p.CategoryName == category))
            {
                List<Product> products = context.Product.Where(p => p.CategoryName == category).ToList();
                return products;
            }
            else
            {
                return new List<Product>();
            }

        }

        public Product GetProductByID(int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            return product;
        }

        public void AddPayment(Payment payment)
        {
            context.Payment.Add(payment);
            context.SaveChanges();
        }

        public void CreatePromocode(Promocode promocode)
        {
            context.Promocode.Add(promocode);
            context.SaveChanges();
        }

        public List<Promocode> GetAllPromocodes()
        {
            List<Promocode> promocodes = context.Promocode.ToList();
            return promocodes;
        }

        public void RemovePromocode(string promocodeName)
        {
            Promocode promocode = context.Promocode.First(p => p.PromocodeName == promocodeName);
            context.Promocode.Remove(promocode);
            context.SaveChanges();
        }

        public void CreateUser(ApplicationUser user)
        {
            context.User.Add(user);
            context.SaveChanges();
        }

        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> users = context.User.ToList();
            return users;
        }

        public void RemoveUser(string userId)
        {
            ApplicationUser user = context.User.First(p => p.Id == userId);
            context.User.Remove(user);
            context.SaveChanges();
        }

        public void RemoveProduct(int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            context.Product.Remove(product);
            context.SaveChanges();
        }

        public void AddPromotion(decimal newPrice, int productID)
        {
            Product product = context.Product.First(p => p.ProductID == productID);
            product.IsInPromotion = true;
            context.Update(product);
            Promotion promotion = new Promotion(productID, newPrice);
            context.Promotion.Add(promotion);
            context.SaveChanges();
        }

        public void AddReview(int productId, string userId, int rating, string comment)
        {
            var review = new Review
            {
                ProductID = productId,
                UserID = userId,
                Rating = rating,
                Comment = comment,
                CreatedDate = DateTime.Now
            };

            context.Review.Add(review);
            context.SaveChanges();
        }

        public List<Review> GetAllReviews(int productId)
        {
            return context.Review
                   .Where(r => r.ProductID == productId)
                   .Include(r => r.User)
                   .ToList();
        }

        public decimal GetProductAverageRating(int productId)
        {
            var averageRating = context.Review
                .Where(r => r.ProductID == productId)
                .Average(r => r.Rating);

            return (decimal)Math.Round(averageRating, 2);
        }

        public List<Promotion> GetAllPromotions()
        {
            List<Promotion> promotions = context.Promotion.ToList();
            return promotions;
        }

        public void RemovePromotion(int productID, int promotionID)
        {
            RemoveProduct(productID);
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

        public ApplicationUser GetUserByID()//public ApplicationUser GetUserByID(int ID)
        {
            ApplicationUser user = context.User.First();//ApplicationUser user = context.User.First(p => p.ID == ID);
            return user;
        }


    }
}
