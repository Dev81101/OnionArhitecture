using Bookstore.Domain.Entities;
using Bookstore.Service;
using Bookstore.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;
        private readonly OrderService _orderService;

        public OrderController(ShoppingCartService shoppingCartService, OrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders(); // Get authors
            return View(orders); // Return authors list to the view
        }

        // GET: Create Order Page
        [HttpGet]
        public IActionResult CreateOrder(int shoppingCartId)
        {
            // Get the shopping cart by ID
            var shoppingCart = _shoppingCartService.GetShoppingCartById(shoppingCartId);

            // If the cart doesn't exist, redirect back to the shopping cart page
            if (shoppingCart == null)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            // Create an order object and set the total price from the cart
            var order = new Order
            {
                ShoppingCartId = shoppingCartId,
                TotalPrice = shoppingCart.TotalPrice
            };

            // Return the order creation form with the cart details
            return View(order);
        }

        // POST: Create Order Action
        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            order.ShoppingCartId = 1;
            if (ModelState.IsValid)
            {
                // Save the order
                _orderService.CreateOrder(order);
                // Redirect to a confirmation page after order is placed
                return RedirectToAction("OrderConfirmation");
            }

            // If validation fails, return the same view with errors
            return View(order);
        }

        // Order Confirmation Page
        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
