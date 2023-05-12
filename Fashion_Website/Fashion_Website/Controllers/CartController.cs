using Fashion_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashion_Website.Models.shoppingCart;

namespace Fashion_Website.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SanPhamGH()
        {
            fashionDBEntities db = new fashionDBEntities();
            var cart = Cart.GetCart();
            var cartItems = cart.Items?.GroupBy(item => new { item.ProductId, item.ProductSize });
            if (cartItems == null || cartItems.Count() == 0)
            {
                ViewBag.Message = "Your cart is empty.";
                return View();
            }

            var cartViewModel = new List<CartItemViewModel>();
            foreach (var itemGroup in cartItems)
            {
                var product = db.SANPHAMs.Find(itemGroup.Key.ProductId);
                var cartItemViewModel = new CartItemViewModel
                {
                    ProductId = itemGroup.Key.ProductId,
                    ProductName = product.TenSP,
                    ProductImg = product.HinhSP,
                    ProductSize = itemGroup.Key.ProductSize,
                    Quantity = itemGroup.Sum(i => i.Quantity),
                    Price = itemGroup.First().Price,
                    Subtotal = itemGroup.Sum(i => i.Quantity * i.Price)
                };
                cartViewModel.Add(cartItemViewModel);
            }

            return View(cartViewModel);
        }


        public ActionResult AddToCart(string productId, int quantity, string size)
        {
            fashionDBEntities db = new fashionDBEntities();
            var product = db.SANPHAMs.Find(productId);

            CartItem cartItem = new CartItem
            {
                ProductId = product.MaSP,
                ProductName = product.TenSP,
                ProductImg = product.HinhSP,
                ProductSize = size,
                Quantity = quantity,
                Price = product.GiaSP
            };

            var cart = Cart.GetCart();
            var existingCartItem = cart.Items != null ? cart.Items.FirstOrDefault(item => item.ProductId == productId && item.ProductSize == size) : null;

            if (existingCartItem != null)
            {
                // Item already exists in cart, update its quantity
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // Item does not exist in cart, add it
                cart.AddItem(cartItem);
            }

            return RedirectToAction("SanPhamGH");
        }


        public ActionResult RemoveFromCart(string productId)
        {
            var cart = Cart.GetCart();
            cart.RemoveItem(productId);

            return RedirectToAction("SanPhamGH");
        }

        public ActionResult ClearCart()
        {
            var cart = Cart.GetCart();
            cart.Clear();

            return RedirectToAction("SanPhamGH");
        }

        public int GetCartTotal()
        {
            var cart = Cart.GetCart();
            int total = 0;
            if (cart != null && cart.Items != null)
            {
                total = cart.Items.Sum(item => item.Quantity);
            }
            return total;
        }

        public ActionResult ThanhToan()
        {
            fashionDBEntities db = new fashionDBEntities();
            // Get the current cart
            var cart = Cart.GetCart();

            // Group the cart items by product and size
            var cartItems = cart.Items.GroupBy(item => new { item.ProductId, item.ProductSize });

            // Create a view model containing the cart items
            var cartViewModel = new List<CartItemViewModel>();
            foreach (var itemGroup in cartItems)
            {
                var product = db.SANPHAMs.Find(itemGroup.Key.ProductId);
                var cartItemViewModel = new CartItemViewModel
                {
                    ProductId = itemGroup.Key.ProductId,
                    ProductName = product.TenSP,
                    ProductImg = product.HinhSP,
                    ProductSize = itemGroup.Key.ProductSize,
                    Quantity = itemGroup.Sum(i => i.Quantity),
                    Price = itemGroup.First().Price,
                    Subtotal = itemGroup.Sum(i => i.Quantity * i.Price)
                };
                cartViewModel.Add(cartItemViewModel);
            }

            // Pass the cart items to the view
            return View(cartViewModel);
        }

        [HttpPost]
        public ActionResult ProcessPayment(PaymentViewModel model)
        {
            fashionDBEntities db = new fashionDBEntities();
            // Get the current cart
            var cart = Cart.GetCart();

            // Ensure that the model state is valid
            if (ModelState.IsValid)
            {
                // TODO: Implement payment processing logic here

                // Clear the cart
                cart.Clear();

                // Return a view indicating that payment was successful
                return View("PaymentSuccess");
            }
            else
            {
                // If the model state is not valid, return the payment view with validation errors
                return View("Payment", model);
            }
        }
    }
}