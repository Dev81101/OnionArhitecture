using Bookstore.Service;
using Bookstore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Bookstore.Service.Services;
using BookStore.Domain.Models;

namespace Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookService _bookService;
        private readonly ShoppingCartService _shoppingCartService;
        private readonly CartItemService _cartItemService;

        //private readonly AuthorService _authorService;
        //private readonly PublisherService _publisherService;


        public HomeController(BookService bookService, ShoppingCartService shoppingCartService, CartItemService cartItemService)
        {
            _bookService = bookService;
            _shoppingCartService = shoppingCartService;
            _cartItemService = cartItemService;
        }

        //GET: Home/Index
        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks(); // Fetch all books
            return View(books);
        }

        //public IActionResult Index(List<int> authorIds, List<int> publisherIds)
        //{
        //    var books = _bookService.GetAllBooks();

        //    if (authorIds != null && authorIds.Any())
        //    {
        //        books = books.Where(b => authorIds.Contains(b.AuthorId)).ToList();
        //    }

        //    if (publisherIds != null && publisherIds.Any())
        //    {
        //        books = books.Where(b => publisherIds.Contains(b.PublisherId)).ToList();
        //    }

        //    var model = new BookViewModel
        //    {
        //        Books = (List<Book>)books,
        //        Authors = (List<Author>)_authorService.GetAllAuthors(),
        //        Publishers = (List<Publisher>)_publisherService.GetAllPublishers(),
        //        SelectedAuthors = authorIds,
        //        SelectedPublishers = publisherIds
        //    };

        //    return View(model);
        //}

        // POST: Home/CreateShoppingCart
        [HttpPost]
        public IActionResult CreateShoppingCart()
        {
            var shoppingCart = _shoppingCartService.CreateShoppingCart(); // Create a new shopping cart
            return RedirectToAction("Index");
        }

        // POST: Home/AddToCart
        
        [HttpPost]
        public IActionResult AddToCart(int bookId)
        {
            var shoppingCart = _shoppingCartService.GetOrCreateCart(); // Get or create shopping cart
            var book = _bookService.GetBookById(bookId); // Get the book details
            var cartItem = new CartItem
            {
                BookId = book.Id,
                BookTitle = book.Title,
                Price = book.Price,
                Quantity = 1
            };

            _cartItemService.AddItemToCart(shoppingCart.Id, cartItem); // Add item to cart
            return RedirectToAction("Index");
        }
    }
}
