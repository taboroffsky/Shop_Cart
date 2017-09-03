using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Cart.Models
{
    public class Cart
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public Cart(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }
    }
}