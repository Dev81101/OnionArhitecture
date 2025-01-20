using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repository.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BookStoreDbContext _context;

        public PublisherRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Add(Publisher entity) => _context.Publishers.Add(entity);

        public void Delete(int id)
        {
            var publisher = _context.Publishers.Find(id);
            if (publisher != null) _context.Publishers.Remove(publisher);
        }

        public IEnumerable<Publisher> GetAll() => _context.Publishers.Include(p => p.Books).ToList();

        public Publisher GetById(int id) => _context.Publishers.Include(p => p.Books).FirstOrDefault(p => p.Id == id);

        public IEnumerable<Publisher> GetPublishersWithBooks() => _context.Publishers.Include(p => p.Books).ToList();

        public void Update(Publisher entity) => _context.Publishers.Update(entity);

        public void SaveChanges() => _context.SaveChanges();
    }
}
