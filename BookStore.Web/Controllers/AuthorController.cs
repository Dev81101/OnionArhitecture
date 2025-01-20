using Bookstore.Domain.Entities;
using Bookstore.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Web.Controllers
{
    // This controller will handle both API and MVC
    public class AuthorsController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        // MVC - GET: Authors
        public IActionResult Index()
        {
            var authors = _authorService.GetAllAuthors(); // Get authors
            return View(authors); // Return authors list to the view
        }

        // API - GET: api/authors
        [ApiController]
        [Route("api/[controller]")]
        public class AuthorsApiController : ControllerBase
        {
            private readonly AuthorService _authorService;

            public AuthorsApiController(AuthorService authorService)
            {
                _authorService = authorService;
            }

            [HttpGet]
            public IActionResult GetAllAuthors() => Ok(_authorService.GetAllAuthors());

            [HttpGet("{id}")]
            public IActionResult GetAuthorById(int id)
            {
                var author = _authorService.GetAuthorById(id);
                if (author == null) return NotFound();
                return Ok(author);
            }

            [HttpPost]
            public IActionResult AddAuthor([FromBody] Author author)
            {
                _authorService.AddAuthor(author);
                return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
            }

            [HttpPut("{id}")]
            public IActionResult UpdateAuthor(int id, [FromBody] Author author)
            {
                if (id != author.Id) return BadRequest();
                _authorService.UpdateAuthor(author);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteAuthor(int id)
            {
                _authorService.DeleteAuthor(id);
                return NoContent();
            }
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View(new Author()); // Return an empty author object to create
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _authorService.AddAuthor(author); // Add new author
                return RedirectToAction(nameof(Index)); // Redirect to list of authors
            }
            return View(author); // Return the form with errors if not valid
        }

        // GET: Authors/Edit/{id}
        public IActionResult Edit(int id)
        {
            var author = _authorService.GetAuthorById(id); // Get the author to edit
            if (author == null)
            {
                return NotFound(); // Return 404 if author not found
            }
            return View(author); // Return the edit view with the author
        }

        // POST: Authors/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest(); // Return BadRequest if IDs don't match
            }

            if (ModelState.IsValid)
            {
                _authorService.UpdateAuthor(author); // Update the author
                return RedirectToAction(nameof(Index)); // Redirect after success
            }

            return View(author); // Return the form with errors
        }

        // GET: Authors/Delete/{id}
        public IActionResult Delete(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound(); // Return 404 if the author is not found
            }

            return View(author); // Return the author to the Delete view
        }

        // POST: Authors/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _authorService.DeleteAuthor(id); // Delete the author
            return RedirectToAction(nameof(Index)); // Redirect to the list of authors after deletion
        }
    }
}
