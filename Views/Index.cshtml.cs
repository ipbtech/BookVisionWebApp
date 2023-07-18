using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.DAL;
using MyFirstWebApp.Domain;
using MyFirstWebApp.Services;

namespace MyFirstWebApp.Views
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext appDbContext;

        public List<Book> Books { get; private set; }

        public IndexModel(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }
        public void OnGet()
        {
            var bs = new BookService(appDbContext);
            Books = bs.GetAll();           
        }
    }
}
