using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repository.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreDbContext _context;

        public AuthorRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Add(Author entity) => _context.Authors.Add(entity);

        public void Delete(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null) _context.Authors.Remove(author);
        }

        public IEnumerable<Author> GetAll() => _context.Authors.Include(a => a.Books).ToList();

        public Author GetById(int id) => _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);

        public IEnumerable<Author> GetAuthorsWithBooks() => _context.Authors.Include(a => a.Books).ToList();

        public void Update(Author entity) => _context.Authors.Update(entity);

        public void SaveChanges() => _context.SaveChanges();
    }
}
