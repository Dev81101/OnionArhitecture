using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }               // The ID of the order
        public string CustomerName { get; set; }   // Name of the customer
        public string CustomerEmail { get; set; }
        
        public string CustomerAddress { get; set; }// Email of the customer
        public DateTime? OrderDate { get; set; }    // Date when the order was placed
        
        public int ShoppingCartId { get; set;}
        public decimal TotalPrice { get; set; }    // Total price of the order

        public ShoppingCart? ShoppingCart { get; set; }
    }

}
