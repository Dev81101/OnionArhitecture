using Bookstore.Domain.Entities;
using Bookstore.Service;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Web.Controllers
{
    [Route("shoppingcart")]
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;
        private readonly CartItemService _cartItemService;

        public ShoppingCartController(ShoppingCartService shoppingCartService, CartItemService cartItemService)
        {
            _shoppingCartService = shoppingCartService;
            _cartItemService = cartItemService;
        }

        // GET: /shoppingcart
        public IActionResult Index()
        {
            // Retrieve the shopping cart for the current user/session
            var shoppingCart = _shoppingCartService.GetShoppingCartById(1);

            if (shoppingCart == null)
            {
                return View(new ShoppingCart { Items = new List<CartItem>() });
            }

            return View(shoppingCart);
        }
        // Create a new shopping cart
        [HttpPost]
        public IActionResult CreateShoppingCart([FromBody] ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                return BadRequest("Shopping cart data is required.");
            }

            _shoppingCartService.CreateShoppingCart(shoppingCart);
            return CreatedAtAction(nameof(GetShoppingCart), new { shoppingCartId = shoppingCart.Id }, shoppingCart);
        }

        // Get a shopping cart by ID
        [HttpGet("{shoppingCartId}")]
        public IActionResult GetShoppingCart(int shoppingCartId)
        {
            var cart = _shoppingCartService.GetShoppingCartById(shoppingCartId);
            if (cart == null)
            {
                return NotFound("Shopping cart not found.");
            }

            return Ok(cart);
        }

        // Add an item to the shopping cart
        [HttpPost("{shoppingCartId}/items")]
        public IActionResult AddItemToCart(int shoppingCartId, [FromBody] CartItem cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest("Cart item data is required.");
            }

            _shoppingCartService.AddItemToCart(shoppingCartId, cartItem);
            return Ok("Item added to cart.");
        }

        // Remove an item from the shopping cart
        //[HttpDelete("{shoppingCartId}/items/{itemId}")]
        //public IActionResult RemoveItemFromCart(int shoppingCartId, int itemId)
        //{
        //    _shoppingCartService.RemoveItemFromCart(shoppingCartId, itemId);
        //    return Ok("Item removed from cart.");
        //}

        [HttpPost("RemoveItemFromCart")]
        public IActionResult RemoveItemFromCart(int shoppingCartId, int itemId)
        {
            if (shoppingCartId <= 0 || itemId <= 0)
            {
                return BadRequest("Invalid shopping cart or item ID.");
            }

            _shoppingCartService.RemoveItemFromCart(shoppingCartId, itemId);

            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public IActionResult ConfirmRemoveItem(int cartItemId)
        //{
        //    if (cartItemId <= 0)
        //    {
        //        return NotFound("Invalid cart item ID.");
        //    }

        //    // Pass the cartItemId to the view
        //    return View(cartItemId);
        //}

        //[HttpPost]
        //public IActionResult RemoveItem(int cartItemId)
        //{
        //    if (cartItemId <= 0)
        //    {
        //        return BadRequest("Invalid cart item ID.");
        //    }

        //    // Perform the removal operation
        //    _cartItemService.RemoveCartItem(cartItemId);

        //    // Redirect back to the shopping cart
        //    return RedirectToAction("Index");
        //}



        // Get all items in the shopping cart
        [HttpGet("{shoppingCartId}/items")]
        public IActionResult GetCartItems(int shoppingCartId)
        {
            var items = _shoppingCartService.GetCartItems(shoppingCartId);
            return Ok(items);
        }

        // Calculate the total price of the shopping cart
        [HttpGet("{shoppingCartId}/total")]
        public IActionResult GetTotalPrice(int shoppingCartId)
        {
            var totalPrice = _shoppingCartService.CalculateTotalPrice(shoppingCartId);
            return Ok(new { TotalPrice = totalPrice });
        }
    }
}
