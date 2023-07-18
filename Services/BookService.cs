using MyFirstWebApp.DAL;
using MyFirstWebApp.Domain;

namespace MyFirstWebApp.Services
{
    public class BookService
    {
        private readonly AppDbContext _dbContext;

        public BookService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public List<Book> GetAll()
        {
            return _dbContext.Books.ToList();
        }
        public Book GetById(long id)
        {
            return _dbContext.Books.FirstOrDefault(x => x.Id == id);
        }
        public async void CreateBookAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
        }
        public void RemoveByIdAsync(long id)
        {
            var removedBook = GetById(id);
            try
            {
                _dbContext.Remove(removedBook);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("Книга не найдена!");
            }
        }
        public void UpdateEntity(Book book)
        {
            _dbContext.Books.Update(book);
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
