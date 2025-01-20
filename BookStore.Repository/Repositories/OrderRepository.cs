using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookStoreDbContext _context;

        public OrderRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        // Add a new Order
        public void Add(Order entity) => _context.Orders.Add(entity);

        // Delete an Order by ID
        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null) _context.Orders.Remove(order);
        }

        // Get all Orders, including their associated ShoppingCart
        public IEnumerable<Order> GetAll() => _context.Orders.Include(o => o.ShoppingCart).ThenInclude(s => s.Items).ThenInclude(i => i.Book).ToList();

        // Get an Order by its ID, including its ShoppingCart
        public Order GetById(int id) => _context.Orders.Include(o => o.ShoppingCart).ThenInclude(s => s.Items).ThenInclude(i => i.Book).FirstOrDefault(o => o.Id == id);

        // Get an Order by its ShoppingCartId
        public Order GetOrderByShoppingCartId(int shoppingCartId) => _context.Orders.Include(o => o.ShoppingCart).ThenInclude(s => s.Items).ThenInclude(i => i.Book).FirstOrDefault(o => o.ShoppingCartId == shoppingCartId);

        // Get all orders placed by the same customer based on customer name (for manual orders)
        public IEnumerable<Order> GetOrdersByCustomerName(string customerName) => _context.Orders.Include(o => o.ShoppingCart).ThenInclude(s => s.Items).ThenInclude(i => i.Book).Where(o => o.CustomerName == customerName).ToList();

        // Update an Order
        public void Update(Order entity) => _context.Orders.Update(entity);

        // Save the changes to the database
        public void SaveChanges() => _context.SaveChanges();
    }
}
