using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Entities
{
    
        public class Book
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public decimal Price { get; set; }
            public int AuthorId { get; set; }
            public int PublisherId { get; set; }
            public string? ImagePath { get; set; } 
            public Author? Author { get; set; }
            public Publisher? Publisher { get; set; }
        }
    

}
