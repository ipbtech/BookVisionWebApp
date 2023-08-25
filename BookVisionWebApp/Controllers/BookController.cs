using BookVisionWebApp.Models;
using BookVisionWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookVisionWebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooks();
            return View(books);
        }

        [HttpGet]
        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.CreateBook(book);
                return RedirectToAction("Index");
            }           
            return View();
        }
    }
}