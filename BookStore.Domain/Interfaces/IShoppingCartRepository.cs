using Bookstore.Domain.Entities;
using System.Collections.Generic;

namespace Bookstore.Domain.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        // Get a shopping cart by its ID (no user reference anymore)
        ShoppingCart GetById(int id);

        // Add an item to a specific shopping cart by its ID
        void AddItemToCart(int shoppingCartId, CartItem item);

        // Remove an item from a specific shopping cart by its ID
        void RemoveItemFromCart(int shoppingCartId, int itemId);

        // Get all shopping carts (optional for admin, etc.)
        IEnumerable<ShoppingCart> GetAll();
    }
}
