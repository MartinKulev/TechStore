
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
                if(product.IsInPromotion)
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

        public Cart GetCartByID(int cartID)
        {
            Cart cart = context.Cart.First(c => c.CartID == cartID);
            return cart;
        }

        public List<Product> GetAllDescriptions(int productId)
        {
            return context.Product
                   .Where(d => d.ProductID == productId)
                   .Include(d => d.Description)
                   .ToList();
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

        public ApplicationUser GetUserByID(string id)
        {
            ApplicationUser user = context.User.First(p => p.Id == id);
            return user;
        }

        public List<Product> GetAllProducts()
        {
            return context.Product.ToList();
        }

        public bool AddProductToCart(int userID, int productID, int quantity, decimal price, string description, string imageURL)
        {
            try
            {
                if (productID <= 0 || quantity <= 0)
                {
                    Console.WriteLine("Invalid input parameters for adding product to cart.");
                    return false;
                }

                var existingCartItem = context.Cart
                    .FirstOrDefault(c => c.UserID == userID && c.ProductID == productID);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                    existingCartItem.Price += price;
                }
                else
                {
                    Cart newCartItem = new Cart
                    {
                        UserID = userID,
                        ProductID = productID,
                        Quantity = quantity,
                        Price = price,
                        Description = description,
                        ImageURL = imageURL
                    };
                    context.Cart.Add(newCartItem);
                }

                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error occurred while adding the product to the cart: {ex.Message}");
                return false;
            }

            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while adding the product to the cart: {ex.Message}");
                return false;
            }
        }

        public Promocode GetPromoCode(string promoCode)
        {
            return context.Promocode.FirstOrDefault(p => p.PromocodeName == promoCode);
        }

        public decimal GetTotalCartPrice(int userId)
        {
            var cartItems = context.Cart
                                  .Where(c => c.UserID == userId)
                                  .ToList();


            decimal totalPrice = 0;
            foreach (var cartItem in cartItems)
            {
                var product = context.Product.FirstOrDefault(p => p.ProductID == cartItem.ProductID);
                if (product != null)
                {
                    decimal itemPrice = product.Price * cartItem.Quantity;
                    totalPrice += itemPrice;
                }
                else
                {
                    throw new InvalidOperationException($"Product with ID {cartItem.ProductID} not found.");
                }
            }

            return totalPrice;
        }

        public int GetUserIdFromCart()
        {
            var cartItem = context.Cart.FirstOrDefault();
            if (cartItem != null)
            {
                return cartItem.UserID;
            }

            throw new InvalidOperationException("UserID not found in Cart table.");
        }


        public List<CartViewModel> GetCartItems(int userId)
        {
            var cartItems = context.Cart.Where(c => c.UserID == userId).ToList();
            var cartItemViewModels = new List<CartViewModel>();

            foreach (var cartItem in cartItems)
            {
                var product = context.Product.FirstOrDefault(p => p.ProductID == cartItem.ProductID);
                if (product != null)
                {
                    cartItemViewModels.Add(new CartViewModel
                    {
                        ProductID = product.ProductID,
                        ImageURL = product.ImageURL,
                        Description = product.Description,
                        Price = product.Price,
                        Quantity = cartItem.Quantity
                    });
                }
            }

            return cartItemViewModels;
        }

        public void AddToTempOrder(TempOrder tempOrder)
        {

            context.TempOrder.Add(tempOrder);
            context.SaveChanges();
        }

        public List<TempOrder> GetTempOrders(int userId)
        {

            return context.TempOrder.Where(to => to.UserID == userId).ToList();
        }

        public void AddOrder(Order order)
        {

            context.Order.Add(order);
            context.SaveChanges();
        }

        public void ClearTempOrders(int userId)
        {

            var tempOrders = context.TempOrder.Where(to => to.UserID == userId);
            context.TempOrder.RemoveRange(tempOrders);
            context.SaveChanges();
        }

        public void DeleteCartItems(int userId)
        {
            var cartItems = context.Cart.Where(c => c.UserID == userId);
            context.Cart.RemoveRange(cartItems);
            context.SaveChanges();
        }

        public List<OrderViewModel> GetOrders(int userId)
        {
            // Retrieve orders associated with the user from the database
            var orders = context.Order
                                .Where(o => o.UserID == userId)
                                .ToList();

            // Initialize a list to store the view models
            var orderViewModels = new List<OrderViewModel>();

            // Iterate through each order and create corresponding view models
            foreach (var order in orders)
            {
                // Retrieve product information for the order
                var product = context.Product.FirstOrDefault(p => p.ProductID == order.ProductID);

                // If product exists, create order view model
                if (product != null)
                {
                    orderViewModels.Add(new OrderViewModel
                    {
                        OrderID = order.OrderID,
                        TotalPrice = order.TotalPrice,
                        CardNum = order.Quantity,
                        OrderTime = order.OrderTime
                    });
                }
            }

            return orderViewModels;
        }
    }
}



    

