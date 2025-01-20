using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;

namespace Bookstore.Service.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly BookStoreDbContext _context;

        public AuthorService(IAuthorRepository authorRepository, BookStoreDbContext context)
        {
            _authorRepository = authorRepository;
            _context = context;
        }

        // Get all authors
        public IEnumerable<Author> GetAllAuthors() => _authorRepository.GetAll();

        // Get an author by ID
        public Author GetAuthorById(int id) => _authorRepository.GetById(id);

        // Get authors with their books
        public IEnumerable<Author> GetAuthorsWithBooks() => _authorRepository.GetAuthorsWithBooks();

        // Add a new author
        public void AddAuthor(Author author)
        {
            _authorRepository.Add(author); // Add the author
            _context.SaveChanges();        // Commit the transaction
        }

        // Update an existing author
        public void UpdateAuthor(Author author)
        {
            _authorRepository.Update(author); // Update the author
            _context.SaveChanges();           // Commit the transaction
        }

        // Delete an author
        public void DeleteAuthor(int id)
        {
            _authorRepository.Delete(id); // Delete the author
            _context.SaveChanges();       // Commit the transaction
        }
    }
}
