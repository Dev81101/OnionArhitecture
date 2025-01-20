using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;
using System;
using System.Collections.Generic;

namespace Bookstore.Service
{
    public class ShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICartItemRepository _cartItemRepository;

        private readonly BookStoreDbContext _context;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ICartItemRepository cartItemRepository, BookStoreDbContext context)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _cartItemRepository = cartItemRepository;
            _context = context;
        }

        // Create a new shopping cart
        public void CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _shoppingCartRepository.Add(shoppingCart);
            _context.SaveChanges();
        }

        // Add an item to the shopping cart
        public void AddItemToCart(int shoppingCartId, CartItem item)
        {
            var cart = _shoppingCartRepository.GetById(shoppingCartId);
            if (cart == null)
            {
                throw new InvalidOperationException("The shopping cart doesn't exist.");
            }

            _cartItemRepository.AddItemToCart(shoppingCartId, item);
        }

        // Remove an item from the shopping cart
        public void RemoveItemFromCart(int shoppingCartId, int itemId)
        {
            var cart = _shoppingCartRepository.GetById(shoppingCartId);
            if (cart == null)
            {
                throw new InvalidOperationException("The shopping cart doesn't exist.");
            }

            _cartItemRepository.RemoveItemFromCart(shoppingCartId, itemId);
        }

        // Get all items in a shopping cart
        public IEnumerable<CartItem> GetCartItems(int shoppingCartId)
        {
            return _cartItemRepository.GetItemsByShoppingCartId(shoppingCartId);
        }

        // Calculate the total price of the shopping cart
        public decimal CalculateTotalPrice(int shoppingCartId)
        {
            var cart = _shoppingCartRepository.GetById(shoppingCartId);
            if (cart == null)
            {
                throw new InvalidOperationException("The shopping cart doesn't exist.");
            }

            return cart.TotalPrice;
        }

        // Get a shopping cart by its ID
        public ShoppingCart GetShoppingCartById(int shoppingCartId)
        {
            return _shoppingCartRepository.GetById(shoppingCartId);
        }

        public ShoppingCart CreateShoppingCart()
        {
            var shoppingCart = new ShoppingCart();
            _shoppingCartRepository.Add(shoppingCart);
            _context.SaveChanges();
            return shoppingCart;
        }

        // Get an existing shopping cart or create a new one if none exists
        public ShoppingCart GetOrCreateCart()
        {
            // Logic to fetch the existing shopping cart, or create one if it doesn't exist
            // You may want to associate a cart with a specific session or user, but since we have no user model, we can create a new cart every time
            var cart = _shoppingCartRepository.GetAll().FirstOrDefault(); // Just for the sake of this example, it gets the first cart.
            if (cart == null)
            {
                cart = CreateShoppingCart();
            }
            return cart;
        }
    }
}
