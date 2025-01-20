using Bookstore.Domain.Entities;
using System.Collections.Generic;

namespace Bookstore.Domain.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        // Get all items in a specific shopping cart
        IEnumerable<CartItem> GetItemsByShoppingCartId(int shoppingCartId);

        // Add a CartItem to a specific ShoppingCart
        void AddItemToCart(int shoppingCartId, CartItem item);

        // Remove a CartItem from a specific ShoppingCart
        void RemoveItemFromCart(int shoppingCartId, int cartItemId);

        // Get a CartItem by its ID
        CartItem GetCartItemById(int cartItemId);
    }
}
