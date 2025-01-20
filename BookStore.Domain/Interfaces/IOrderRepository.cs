using Bookstore.Domain.Entities;
using System.Collections.Generic;

namespace Bookstore.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        // Get an order by its ShoppingCartId
        Order GetOrderByShoppingCartId(int shoppingCartId);

        // Get all orders placed by a customer based on their customer name
        IEnumerable<Order> GetOrdersByCustomerName(string customerName);
    }
}
