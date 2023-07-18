using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.DAL;
using MyFirstWebApp.Services;

namespace MyFirstWebApp.Views
{
    public class RemoveModel : PageModel
    {
        private readonly AppDbContext appDbContext;

        public long BookId { get; private set; }
        public RemoveModel(AppDbContext _dbContext)
        {
            appDbContext = _dbContext;
        }

        public void OnGet(long id)
        {
            BookId = id;
            BookService bs = new BookService(appDbContext);
            bs.RemoveByIdAsync(BookId);
            bs.SaveChangesAsync();
        }
    }
}
