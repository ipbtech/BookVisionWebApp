using BookVisionWebApp.Models;

namespace BookVisionWebApp.Services.Interfaces
{
    public interface IBookService
    {
        public Task<BaseResponce<IEnumerable<Book>>> GetAllBooks();
        public Task<BaseResponce<Book>> GetBookById(long id);
        public Task<BaseResponce<Book>> CreateBook(Book book);
        public Task DeleteBook(Book book);
        public Task<BaseResponce<Book>> EditBook(Book book);
    }
}
