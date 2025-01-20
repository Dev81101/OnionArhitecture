using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;
using System.Collections.Generic;

namespace Bookstore.Service.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly BookStoreDbContext _context;

        public BookService(IBookRepository bookRepository, BookStoreDbContext context)
        {
            _bookRepository = bookRepository;
            _context = context;
        }

        public IEnumerable<Book> GetAllBooks() => _bookRepository.GetAll();

        public Book GetBookById(int id) => _bookRepository.GetById(id);

        public IEnumerable<Book> GetBooksByAuthor(int authorId) => _bookRepository.GetBooksByAuthor(authorId);

        public IEnumerable<Book> GetBooksByPublisher(int publisherId) => _bookRepository.GetBooksByPublisher(publisherId);

        public void AddBook(Book book)
        {
            _bookRepository.Add(book);  // Perform the data operation
            _context.SaveChanges();     // Commit the changes
        }

        public void UpdateBook(Book book)
        {
            _bookRepository.Update(book); // Perform the data operation
            _context.SaveChanges();       // Commit the changes
        }

        public void DeleteBook(int id)
        {
            _bookRepository.Delete(id);  // Perform the data operation
            _context.SaveChanges();      // Commit the changes
        }
    }
}
