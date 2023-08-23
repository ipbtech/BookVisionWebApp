using BookVisionWebApp.DAL.Repository.Interfaces;
using BookVisionWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookVisionWebApp.DAL.Repository
{
    public class BookRepository : IBaseRepository<Book>
    {
        private readonly AppDbContext _dbContext;

        public BookRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetById(Book entity)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == entity.Id);
        }
        
        public async Task Create(Book entity)
        {
            await _dbContext.Books.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Book entity)
        {
            _dbContext.Books.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(Book entity)
        {
            _dbContext.Books.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
