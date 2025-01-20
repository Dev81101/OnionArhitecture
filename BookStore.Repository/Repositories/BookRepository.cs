using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repository.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext _context;

        public BookRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Add(Book entity) => _context.Books.Add(entity);

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null) _context.Books.Remove(book);
        }

        public IEnumerable<Book> GetAll() => _context.Books.Include(b => b.Author).Include(b => b.Publisher).ToList();

        public Book GetById(int id) => _context.Books.Include(b => b.Author).Include(b => b.Publisher).FirstOrDefault(b => b.Id == id);

        public IEnumerable<Book> GetBooksByAuthor(int authorId) => _context.Books.Where(b => b.AuthorId == authorId).ToList();

        public IEnumerable<Book> GetBooksByPublisher(int publisherId) => _context.Books.Where(b => b.PublisherId == publisherId).ToList();

        public void Update(Book entity) => _context.Books.Update(entity);

        public void SaveChanges() => _context.SaveChanges();
    }
}
