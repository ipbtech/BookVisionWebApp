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
            var responce = await _bookService.GetAllBooks();
            return View(responce.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(int id)
        {
            var responce = await _bookService.GetBookById(id);
            if (responce.IsOkay)
            {
                var pathToImage = ImageHelper.GetPathToImageFileForRender(responce.Data.PathToImageFile);
                ViewData.Add("ImageRenderPath", pathToImage);
                return View(responce.Data);
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
                if (!string.IsNullOrEmpty(book.PathToImageFile))
                    book.ImageFileName = book.ImageFile.FileName;

                var responce =  await _bookService.CreateBook(book);
                if (responce.IsOkay)
                {
                    await ImageHelper.SendImageFileToServerAsync(book);                  
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData.Add("Error", responce.ErrorMessage);
                }
            }           
            return View();
        }

        public async Task<IActionResult> DeleteBook(int id)
        {
            var responce = await _bookService.GetBookById(id);
            if (responce.IsOkay)
            {
                ImageHelper.DeleteBookImageOnServer(responce.Data);
                await _bookService.DeleteBook(responce.Data);
            }            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {
            var responce = await _bookService.GetBookById(id);
            if (responce.IsOkay)
            {              
                var pathToImage = ImageHelper.GetPathToImageFileForRender(responce.Data.PathToImageFile);
                ViewData.Add("ImageRenderPath", pathToImage);
                return View(responce.Data);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            if (ModelState.IsValid)
            {              
                var responce = await _bookService.EditBook(book);
                if (responce.IsOkay)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData.Add("Error", responce.ErrorMessage);

                    responce = await _bookService.GetBookById(book.Id);
                    if (responce.IsOkay)
                    {
                        var pathToImage = ImageHelper.GetPathToImageFileForRender(responce.Data.PathToImageFile);
                        ViewData.Add("ImageRenderPath", pathToImage);
                    }
                }
            }
            return View(book);
        }
    }
}