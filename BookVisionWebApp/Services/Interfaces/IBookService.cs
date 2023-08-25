using BookVisionWebApp.Models;

namespace BookVisionWebApp.Services.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBookById(int id);
        public Task CreateBook(Book book);
        public Task DeleteBook(Book book);
        public Task EditBookById(Book book);
    }
}
