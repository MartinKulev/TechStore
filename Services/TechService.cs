﻿using Microsoft.AspNetCore.Authentication;
using TechStore.Data;
using TechStore.Data.Entities;

namespace TechStore.Services
{
    public class TechService
    {
        private TechStoreContext context;

        public TechService(TechStoreContext context)
        {
            this.context = context;
        }

        public void AddCategory(Category category)
        {
            if (!context.Category.Contains(category))
            {
                context.Category.Add(category);
                context.SaveChanges();
            }
        }

        public void AddProduct(Product product)
        {
            context.Product.Add(product);
            context.SaveChanges();
        }

        public List<Product> GetProducts(string category)
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
    }
}
