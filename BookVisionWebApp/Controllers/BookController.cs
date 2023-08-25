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
        public IActionResult Index()
        {
            
            
            return View();
        }
    }
}