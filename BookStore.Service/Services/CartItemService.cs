using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;

namespace Bookstore.Service
{
    public class CartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;

        private readonly BookStoreDbContext _context;

        public CartItemService(ICartItemRepository cartItemRepository, BookStoreDbContext context)
        {
            _cartItemRepository = cartItemRepository;
            _context = context; 
        }

        // Add a new cart item
        public void AddCartItem(CartItem cartItem)
        {
            _cartItemRepository.Add(cartItem);
            _context.SaveChanges();
        }

        // Remove a cart item by its ID
        public void RemoveCartItem(int itemId)
        {
            _cartItemRepository.Delete(itemId);
            _context.SaveChanges();
        }

        // Get a cart item by its ID
        public CartItem GetCartItemById(int itemId)
        {
            return _cartItemRepository.GetCartItemById(itemId);
        }

        // Update a cart item (e.g., change quantity or price)
        public void UpdateCartItem(CartItem cartItem)
        {
            _cartItemRepository.Update(cartItem);
            _context.SaveChanges();
        }

        public void AddItemToCart(int shoppingCartId, CartItem cartItem)
        {
            cartItem.ShoppingCartId = shoppingCartId;
            _cartItemRepository.Add(cartItem);
            _context.SaveChanges();
        }
    }
}
