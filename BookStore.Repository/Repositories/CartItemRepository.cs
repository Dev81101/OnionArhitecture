using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Repository.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly BookStoreDbContext _context;

        public CartItemRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        // Add a CartItem to a ShoppingCart
        public void Add(CartItem entity)
        {
            var cart = _context.ShoppingCarts.Include(s => s.Items).FirstOrDefault(s => s.Id == entity.ShoppingCartId);
            if (cart != null)
            {
                cart.Items.Add(entity);
                SaveChanges();
            }
        }

        // Add a CartItem to a ShoppingCart by ShoppingCartId
        public void AddItemToCart(int shoppingCartId, CartItem item)
        {
            var cart = _context.ShoppingCarts.Include(s => s.Items).FirstOrDefault(s => s.Id == shoppingCartId);
            if (cart != null)
            {
                cart.Items.Add(item); // Add the item to the cart
                SaveChanges();
            }
        }

        // Delete a CartItem by its ID
        public void Delete(int id)
        {
            var item = _context.CartItems.Find(id);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                SaveChanges();
            }
        }

        // Get all CartItems
        public IEnumerable<CartItem> GetAll()
        {
            return _context.CartItems.Include(c => c.Book).ToList();
        }

        // Get all CartItems by ShoppingCartId
        public IEnumerable<CartItem> GetItemsByShoppingCartId(int shoppingCartId)
        {
            return _context.CartItems
                .Where(c => c.ShoppingCartId == shoppingCartId)
                .Include(c => c.Book) // Include book details
                .ToList();
        }

        // Get a CartItem by its ID
        public CartItem GetById(int id)
        {
            return _context.CartItems
                .Include(c => c.Book) // Include book details
                .FirstOrDefault(c => c.Id == id);
        }

        // Get a specific CartItem by itemId
        public CartItem GetCartItemById(int itemId)
        {
            return _context.CartItems
                .Include(c => c.Book)
                .FirstOrDefault(c => c.Id == itemId);
        }

        // Remove a CartItem from the Cart by its itemId and shoppingCartId
        public void RemoveItemFromCart(int shoppingCartId, int itemId)
        {
            var cart = _context.ShoppingCarts.Include(s => s.Items).FirstOrDefault(s => s.Id == shoppingCartId);
            if (cart != null)
            {
                var item = cart.Items.FirstOrDefault(c => c.Id == itemId);
                if (item != null)
                {
                    cart.Items.Remove(item);
                    SaveChanges();
                }
            }
        }

        // Update a CartItem (e.g., change quantity or price)
        public void Update(CartItem entity)
        {
            var existingItem = _context.CartItems.Find(entity.Id);
            if (existingItem != null)
            {
                existingItem.Quantity = entity.Quantity; // Update quantity or other fields as needed
                existingItem.Price = entity.Price; // Update price if necessary
                SaveChanges();
            }
        }

        // Save changes to the context
        public void SaveChanges() => _context.SaveChanges();
    }
}
