using Bookstore.Domain.Entities;
using Bookstore.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System;

namespace Bookstore.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;
        private readonly PublisherService _publisherService;

        // Constructor for DI
        public BooksController(BookService bookService, AuthorService authorService, PublisherService publisherService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
        }

        // GET: Books
        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks(); // Get all books from the service
            return View(books); // Pass books to the Books/Index.cshtml view
        }

        // GET: Books/Create
        public IActionResult Create()
        {
           
            // Get list of authors and publishers from the service
            var authors = _authorService.GetAllAuthors() ?? new List<Author>(); // Return empty list if null
            var publishers = _publisherService.GetAllPublishers() ?? new List<Publisher>(); // Return empty list if null

            // Populate ViewBag with author and publisher lists
            ViewBag.Authors = new SelectList(authors, "Id", "Name"); // Adjust based on your Author model
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name"); // Adjust based on your Publisher model

            return View(new Book());
        
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    // Ensure the target directory exists
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Save the image to the folder
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }

                    // Assign the image path to the book
                    book.ImagePath = "/images/books/" + fileName;
                }

                _bookService.AddBook(book); // Save the book
                return RedirectToAction(nameof(Index)); // Redirect after successful create
            }

            // In case of validation failure, reload authors and publishers
            var authors = _authorService.GetAllAuthors();
            var publishers = _publisherService.GetAllPublishers();
            ViewBag.Authors = new SelectList(authors, "Id", "Name");
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name");

            return View(book); // Return the view if the model state is invalid
        }


        // GET: Books/Edit/{id}
        public IActionResult Edit(int id)
        {
            var book = _bookService.GetBookById(id); // Get the book to edit
            if (book == null)
            {
                return NotFound(); // Return 404 if the book is not found
            }

            // Get list of authors and publishers to populate dropdowns
            var authors = _authorService.GetAllAuthors();
            var publishers = _publisherService.GetAllPublishers();

            ViewBag.Authors = new SelectList(authors, "Id", "Name", book.AuthorId);
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name", book.PublisherId);

            return View(book); // Pass the book to the Edit view
        }

        // POST: Books/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book, IFormFile Image)
        {
            if (id != book.Id)
            {
                return BadRequest(); // Ensure the id matches
            }

            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    // Save the new image
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }

                    // Update the book's ImagePath with the new image
                    book.ImagePath = "/images/books/" + fileName;
                }
                else
                {
                    // Keep the existing image path if no new image is uploaded
                    var existingBook = _bookService.GetBookById(id);
                    if (existingBook != null)
                    {
                        book.ImagePath = existingBook.ImagePath; // Retain the original image path
                    }
                }

                _bookService.UpdateBook(book); // Update the book
                return RedirectToAction(nameof(Index)); // Redirect after saving
            }

            // If validation fails, reload authors and publishers
            var authors = _authorService.GetAllAuthors();
            var publishers = _publisherService.GetAllPublishers();
            ViewBag.Authors = new SelectList(authors, "Id", "Name", book.AuthorId);
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name", book.PublisherId);

            return View(book); // Return the view with validation errors
        }


        // GET: Books/Delete/{id}
        public IActionResult Delete(int id)
        {
            var book = _bookService.GetBookById(id); // Get the book to delete
            if (book == null)
            {
                return NotFound(); // Return 404 if the book is not found
            }
            return View(book); // Pass the book to the Delete confirmation view
        }

        // POST: Books/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            var book = _bookService.GetBookById(id); // Get the book to delete
            if (book == null)
            {
                return NotFound(); // Return 404 if the book is not found
            }

            _bookService.DeleteBook(id); // Delete the book
            return RedirectToAction(nameof(Index)); // Redirect to the book list
        }

        // GET: api/Books (API endpoint)
        [HttpGet]
        [Route("api/books")]
        public IActionResult GetAllBooksApi()
        {
            var books = _bookService.GetAllBooks(); // Get all books
            return Ok(books); // Return books as JSON
        }

        // GET: api/Books/{id} (API endpoint)
        [HttpGet]
        [Route("api/books/{id}")]
        public IActionResult GetBookByIdApi(int id)
        {
            var book = _bookService.GetBookById(id); // Get the book
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" }); // Return 404 if not found
            }
            return Ok(book); // Return the book as JSON
        }
    }
}
