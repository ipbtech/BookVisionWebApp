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
        
        public async Task<BaseResponce<IEnumerable<Book>>> GetAllBooks()
        {
            var responce = new BaseResponce<IEnumerable<Book>>()
            {
                IsOkay = true,
                Data = await _dbContext.Books.ToListAsync(),
            };
            return responce;
        }

        public async Task<BaseResponce<Book>> GetBookById(long id)
        {
            Book book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book != null)
            {
                return new BaseResponce<Book>()
                {
                    IsOkay = true,
                    Data = book,
                };
            }
            else
            {
                return new BaseResponce<Book>()
                {
                    IsOkay = false,
                    ErrorMessage = "Книга не найдена"
                };
            }
        }

        public async Task<BaseResponce<Book>> CreateBook(Book book)
        {
            var booksCollection = await _dbContext.Books.ToListAsync();
            if (!booksCollection.Contains(book, new BookEqualityComparer()))
            {
                await _dbContext.Books.AddAsync(book);
                await _dbContext.SaveChangesAsync();

                return new BaseResponce<Book>()
                {
                    IsOkay = true,
                    Data = book
                };
            }
            else
            {
                return new BaseResponce<Book>()
                {
                    IsOkay = false,
                    ErrorMessage = "Такая книга уже существует в базе данных"
                };
            }
        }

        public async Task DeleteBook(Book book)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BaseResponce<Book>> EditBook(Book newBook)
        {
            var booksCollection = await _dbContext.Books.ToListAsync();

            var modifiedColl = new List<Book>();
            booksCollection.ForEach(bk =>
            {
                if (bk.Id != newBook.Id)
                    modifiedColl.Add(bk);
            });

            Book oldBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == newBook.Id);
            if (!modifiedColl.Contains(newBook, new BookEqualityComparer()))
            {
                if (newBook.ImageFile != null)
                {
                    if (oldBook.ImageFileName != newBook.ImageFile.FileName)
                    {
                        ImageHelper.DeleteBookImageOnServer(oldBook);

                        newBook.PathToImageFile = ImageHelper.GetPathToImageFileForServer(newBook.ImageFile);
                        oldBook.PathToImageFile = newBook.PathToImageFile;
                        oldBook.ImageFileName = newBook.ImageFile.FileName;

                        await ImageHelper.SendImageFileToServerAsync(newBook);
                    }
                }

                oldBook.Author = newBook.Author;
                oldBook.Title = newBook.Title;
                oldBook.Price = newBook.Price;
                oldBook.Description = newBook.Description;

                _dbContext.Books.Update(oldBook);
                await _dbContext.SaveChangesAsync();

                return new BaseResponce<Book>()
                {
                    IsOkay = true,
                    Data = oldBook
                };
            }
            else
            {
                return new BaseResponce<Book>()
                {
                    IsOkay = false,
                    ErrorMessage = "Такая книга уже существует в базе данных"
                };
            }
        }
    }
}
