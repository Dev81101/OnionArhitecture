using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }        // The ID of the book
        public string BookTitle { get; set; }  // The title of the book
        public decimal Price { get; set; }     // The price of the book
        public int Quantity { get; set; }      // Quantity of the book in the cart
        public int ShoppingCartId { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }
        public Book? Book { get; set; }
    }
}