
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechStore.Data;
using TechStore.Models.Entities;
using TechStore.Models.ViewModels;
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
                if (product.IsInPromotion)
                {
                    Promotion promotion = context.Promotion.First(p => p.ProductID == product.ProductID);
                    context.Promotion.Remove(promotion);
                }
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
                List<Product> products = context.Product.Where(p => p.CategoryName == category).OrderBy(p => p.Price).ToList();
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

        public List<Product> GetAllDescriptions(int productId)
        {
            return context.Product
                   .Where(d => d.ProductID == productId)
                   .Include(d => d.Description)
                   .ToList();
        }

        public int AddPayment(Payment payment)
        {
            context.Payment.Add(payment);
            context.SaveChanges();

            context.Entry(payment).Reload();
            int generatedKey = payment.PaymentID;
            return generatedKey;
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
            List<Promotion> promotions = context.Promotion.OrderBy(p => p.NewPrice).ToList();
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

        public ApplicationUser GetUserByID(string id)
        {
            ApplicationUser user = context.User.First(p => p.Id == id);
            return user;
        }

        public List<Product> GetAllProducts()
        {
            return context.Product.ToList();
        }

        public void EditUser(string userID, string newUserName, string newFirstName, string newLastName, string newEmail, string newPhoneNumber)
        {
            ApplicationUser oldUser = GetUserByID(userID);
            if (newUserName != null)
            {
                oldUser.UserName = newUserName;
            }
            if (newFirstName != null)
            {
                oldUser.FirstName = newFirstName;
            }
            if (newLastName != null)
            {
                oldUser.LastName = newLastName;
            }
            if (newEmail != null)
            {
                oldUser.Email = newEmail;
            }
            if (newPhoneNumber != null)
            {
                oldUser.PhoneNumber = newPhoneNumber;
            }

            context.Update(oldUser);
            context.SaveChanges();
        }

        public void EditPromocode(int promocodeID, string newPromocodeName, decimal newPromocodeDiscount)
        {
            Promocode promocode = GetPromocodeByID(promocodeID);
            if (newPromocodeName != null)
            {
                promocode.PromocodeName = newPromocodeName;
            }
            if (newPromocodeDiscount != 0)
            {
                promocode.Discount = newPromocodeDiscount;
            }

            context.Update(promocode);
            context.SaveChanges();
        }

        public void EditCategory(int categoryID, string newCategoryName)
        {
            Category category = GetCategoryByID(categoryID);
            if (newCategoryName != null)
            {
                category.CategoryName = newCategoryName;
            }

            context.Update(category);
            context.SaveChanges();
        }

        public Promocode GetPromocodeByID(int promocodeID)
        {
            Promocode promocode = context.Promocode.First(p => p.PromocodeId == promocodeID);
            return promocode;
        }

        public Category GetCategoryByID(int categoryID)
        {
            Category category = context.Category.First(p => p.CategoryId == categoryID);
            return category;
        }

        public void AddItemToCart(string userID, int productID)
        {
            Cart cart = new Cart(userID, productID, 1);
            if (context.Cart.Any(cart => cart.UserID == userID && cart.ProductID == productID && cart.PaymentID == 0))
            {
                Cart existingCart = GetCartByUserIDProductID(userID, productID);
                existingCart.Quantity++;
                context.Update(existingCart);
                context.SaveChanges();
            }
            else
            {
                context.Add(cart);
                context.SaveChanges();
            }
        }

        public Cart GetCartByUserIDProductID(string userID, int productID)
        {
            Cart cart = context.Cart.First(p => p.UserID == userID && p.ProductID == productID);
            return cart;
        }

        public List<Cart> GetAllCartProductsByUserID(string userID)
        {
            List<Cart> carts = context.Cart.Where(p => p.UserID == userID && p.PaymentID == 0).ToList();
            return carts;
        }

        public void UpdateCartsByUserID(string userID, int paymentID)
        {
            var cartsToUpdate = context.Cart.Where(p => p.UserID == userID && p.PaymentID == 0);
            foreach (var cart in cartsToUpdate)
            {
                cart.PaymentID = paymentID;
            }
            context.SaveChanges();
        }

        public List<Payment> GetAllPaymentsByUserID(string usedID)
        {
            List<Payment> payments = context.Payment.Where(p => p.UserID == usedID).ToList();
            return payments;
        }

        public void RemoveItemFromCart(string userID, int productID)
        {
            Cart cart = context.Cart.First(p => p.UserID == userID && p.ProductID == productID && p.PaymentID == 0);

            if (cart.Quantity > 1)
            {
                cart.Quantity--;
                context.Update(cart);
                context.SaveChanges();
            }
            else
            {
                context.Remove(cart);
                context.SaveChanges();
            }
        }

        public Payment GetPaymentByID(int paymentID)
        {
            Payment payment = context.Payment.First(p => p.PaymentID == paymentID);
            return payment;
        }

        public List<Cart> GetAllCartsByPaymentID(int paymentID)
        {
            List<Cart> carts = context.Cart.Where(p => p.PaymentID == paymentID).ToList();
            return carts;
        }

    }
}





