using Bookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class BookViewModel
    {
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<int> SelectedAuthors { get; set; } = new List<int>();
        public List<int> SelectedPublishers { get; set; } = new List<int>();
    }
}
