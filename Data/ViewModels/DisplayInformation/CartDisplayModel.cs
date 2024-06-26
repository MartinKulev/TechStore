﻿using TechStore.Data.Entities;

namespace TechStore.Data.ViewModels.DisplayInformation
{
    public class CartDisplayModel
    {
        public CartDisplayModel(List<Cart> carts, List<Product> products)
        {
            Carts = carts;
            Products = products;
        }

        public List<Cart> Carts { get; set; }

        public List<Product> Products { get; set; }
    }
}
