using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bookstore.Repository.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly BookStoreDbContext _context;

        public ShoppingCartRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        // Add a new shopping cart
        public void Add(ShoppingCart entity) => _context.ShoppingCarts.Add(entity);

        // Delete a shopping cart by its ID
        public void Delete(int id)
        {
            var cart = _context.ShoppingCarts.Find(id);
            if (cart != null) _context.ShoppingCarts.Remove(cart);
        }

        // Get a shopping cart by its ID (includes cart items and related books)
        public ShoppingCart GetById(int id) => _context.ShoppingCarts
            .Include(s => s.Items)
            .ThenInclude(c => c.Book)
            .FirstOrDefault(s => s.Id == id);

        // Add an item to a specific shopping cart
        public void AddItemToCart(int shoppingCartId, CartItem item)
        {
            var cart = GetById(shoppingCartId);
            if (cart != null)
            {
                cart.Items.Add(item);
                SaveChanges();
            }
        }

        // Remove an item from a specific shopping cart
        public void RemoveItemFromCart(int shoppingCartId, int itemId)
        {
            var cart = GetById(shoppingCartId);
            if (cart != null)
            {
                var item = cart.Items.FirstOrDefault(c => c.BookId == itemId);
                if (item != null)
                {
                    cart.Items.Remove(item);
                    SaveChanges();
                }
            }
        }

        // Update a shopping cart (including items)
        public void Update(ShoppingCart entity) => _context.ShoppingCarts.Update(entity);

        // Save changes to the context
        public void SaveChanges() => _context.SaveChanges();

        // Retrieve all shopping carts (if needed)
        public IEnumerable<ShoppingCart> GetAll() => _context.ShoppingCarts
            .Include(s => s.Items)
            .ThenInclude(i => i.Book)
            .ToList();
    }
}
