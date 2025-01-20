using Bookstore.Domain.Entities;
using Bookstore.Domain.Interfaces;
using BookStore.Repository.Data;

namespace Bookstore.Service.Services
{
    public class PublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly BookStoreDbContext _context;

        public PublisherService(IPublisherRepository publisherRepository, BookStoreDbContext context)
        {
            _publisherRepository = publisherRepository;
            _context = context;
        }

        // Get all publishers
        public IEnumerable<Publisher> GetAllPublishers() => _publisherRepository.GetAll();

        // Get a publisher by ID
        public Publisher GetPublisherById(int id) => _publisherRepository.GetById(id);

        // Get publishers with their books
        public IEnumerable<Publisher> GetPublishersWithBooks() => _publisherRepository.GetPublishersWithBooks();

        // Add a new publisher
        public void AddPublisher(Publisher publisher)
        {
            _publisherRepository.Add(publisher); // Add the publisher
            _context.SaveChanges();              // Commit the transaction
        }

        // Update an existing publisher
        public void UpdatePublisher(Publisher publisher)
        {
            _publisherRepository.Update(publisher); // Update the publisher
            _context.SaveChanges();                 // Commit the transaction
        }

        // Delete a publisher
        public void DeletePublisher(int id)
        {
            _publisherRepository.Delete(id); // Delete the publisher
            _context.SaveChanges();          // Commit the transaction
        }
    }
}
