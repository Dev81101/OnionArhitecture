using Bookstore.Domain.Entities;
using Bookstore.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Web.Controllers
{
    public class PublishersController : Controller
    {
        private readonly PublisherService _publisherService;

        public PublishersController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        // GET: Publishers
        public IActionResult Index()
        {
            var publishers = _publisherService.GetAllPublishers(); // Get all publishers
            return View(publishers); // Pass the list of publishers to the view
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View(); // Return the create view
        }

        // POST: Publishers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _publisherService.AddPublisher(publisher); // Add the publisher to the database
                return RedirectToAction(nameof(Index)); // Redirect to the publisher list after creation
            }
            return View(publisher); // If validation fails, return to the create view with validation errors
        }

        // GET: Publishers/Edit/{id}
        public IActionResult Edit(int id)
        {
            var publisher = _publisherService.GetPublisherById(id); // Get the publisher by id
            if (publisher == null)
            {
                return NotFound(); // Return 404 if publisher is not found
            }

            return View(publisher); // Return the edit view with the publisher details
        }

        // POST: Publishers/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest(); // Return BadRequest if ids don't match
            }

            if (ModelState.IsValid)
            {
                _publisherService.UpdatePublisher(publisher); // Update the publisher
                return RedirectToAction(nameof(Index)); // Redirect to the publisher list after update
            }
            return View(publisher); // If validation fails, return to the edit view with validation errors
        }

        // GET: Publishers/Delete/{id}
        public IActionResult Delete(int id)
        {
            var publisher = _publisherService.GetPublisherById(id); // Get the publisher to delete
            if (publisher == null)
            {
                return NotFound(); // Return 404 if publisher is not found
            }
            return View(publisher); // Return the delete confirmation view
        }

        // POST: Publishers/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _publisherService.DeletePublisher(id); // Delete the publisher
            return RedirectToAction(nameof(Index)); // Redirect to the publisher list after deletion
        }
    }
}
