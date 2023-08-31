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
        public async Task<IActionResult> GetBook(int id)
        {
            Book book = await _bookService.GetBookById(id);
            if (book != null)
            {
                var pathToImage = ImageHelper.GetPathToImageFileForRender(book.PathToImageFile);
                ViewData.Add("ImageRenderPath", pathToImage);
                return View(book);
            }
            return RedirectToAction("Index");
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
                book.PathToImageFile = ImageHelper.GetPathToImageFileForServer(book.ImageFile);
                book.ImageFileName = book.ImageFile.FileName;
                bool isCreated = await _bookService.CreateBook(book);
                if (isCreated)
                {
                    await ImageHelper.SendImageFileToServerAsync(book);                  
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData.Add("Error", "Такая книга уже есть в нашей базе");
                }
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

        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {
            Book book = await _bookService.GetBookById(id);
            if (book != null)
            {              
                var pathToImage = ImageHelper.GetPathToImageFileForRender(book.PathToImageFile);
                ViewData.Add("ImageRenderPath", pathToImage);
                return View(book);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            if (ModelState.IsValid)
            {              
                var isEditable = await _bookService.EditBook(book);
                if (isEditable)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData.Add("Error", "Такая книга уже есть в нашей базе");

                    var pathToImage = ImageHelper.GetPathToImageFileForRender(book.PathToImageFile);
                    ViewData.Add("ImageRenderPath", pathToImage);
                }
            }
            return View(book);
        }
    }
}