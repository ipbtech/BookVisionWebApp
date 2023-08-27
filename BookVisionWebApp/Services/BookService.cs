using BookVisionWebApp.DAL;
using BookVisionWebApp.Models;
using BookVisionWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookVisionWebApp.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _dbContext;

        public BookService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }
        public async Task<Book> GetBookById(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> CreateBook(Book book)
        {
            var booksCollection = await _dbContext.Books.ToListAsync();
            if (!booksCollection.Contains(book, new BookEqualityComparer()))
            {
                await _dbContext.Books.AddAsync(book);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task DeleteBook(Book book)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> EditBook(Book book)
        {
            var booksCollection = await _dbContext.Books.ToListAsync();
            if (!booksCollection.Contains(book, new BookEqualityComparer()))
            {
                _dbContext.Books.Update(book);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
