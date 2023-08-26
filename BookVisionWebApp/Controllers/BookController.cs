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

        public async Task<IActionResult> DeleteBook(int id)
        {
            Book book = await _bookService.GetBookById(id);
            if (book != null)
            {
                await _bookService.DeleteBook(book);
            }            
            return RedirectToAction("Index");
        }
    }
}