using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;
using System;
using System.Collections.Generic;

namespace Bookstore.Service
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        private readonly BookStoreDbContext _context;

        public OrderService(IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository, BookStoreDbContext context)
        {
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _context = context;
        }

        // Create a new order (manually typed customer details)
        public void CreateOrder(Order order)
        {
            // Get the shopping cart for the provided ShoppingCartId
            var cart = _shoppingCartRepository.GetById(order.ShoppingCartId);
            if (cart == null || cart.Items.Count == 0)
            {
                throw new InvalidOperationException("The shopping cart is empty or doesn't exist.");
            }

            // Calculate the total price for the order
            order.TotalPrice = cart.TotalPrice;

            // Save the order to the repository
            _orderRepository.Add(order);
            _context.SaveChanges();
        }

        // Get an order by its ID
        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetById(orderId);
        }

        // Get all orders
        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        // Delete an order by its ID
        public void DeleteOrder(int orderId)
        {
            _orderRepository.Delete(orderId);
            _context.SaveChanges();
        }
    }
}
