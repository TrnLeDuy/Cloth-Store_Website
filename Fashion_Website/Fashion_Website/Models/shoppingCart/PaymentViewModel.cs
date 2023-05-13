using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion_Website.Models.shoppingCart
{
    public class PaymentViewModel
    {
        public decimal TotalAmount { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string CVV { get; set; }

        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}