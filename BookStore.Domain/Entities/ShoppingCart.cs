using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();  // List of items in the cart

        // Calculate the total price of all items in the cart
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
