using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion_Website.Models.shoppingCart
{
    public class CartItemGroup
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public decimal ProductPrice { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}