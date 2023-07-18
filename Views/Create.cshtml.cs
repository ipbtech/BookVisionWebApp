using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.DAL;
using MyFirstWebApp.Domain;
using MyFirstWebApp.Services;

namespace MyFirstWebApp.Views
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext appDbContext;

        [BindProperty]
        private Book NewBook { get; set; }
        public string ResultMessage { get; private set; } = string.Empty;
        public string ErrorMessage { get; private set; } = string.Empty;

        public CreateModel(AppDbContext _dbContext)
        {
            appDbContext = _dbContext;
        }
        public void OnPost(string title, string author, string description, string price)
        {
            double verifyprice;
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
            {
                ResultMessage = "Книга не добавлена!";
                ErrorMessage = "Название и автор не могут быть пустыми.";
            }
            else if (!double.TryParse(price, out verifyprice))
            {
                ResultMessage = "Книга не добавлена!";
                ErrorMessage = "Введенная стоимость некорректна.";
            }
            else
            {
                NewBook = new Book(title, author, verifyprice);
                if (!string.IsNullOrEmpty(description))
                {
                    NewBook.Description = description;
                }

                BookService bs = new BookService(appDbContext);
                bs.CreateBookAsync(NewBook);
                bs.SaveChangesAsync();

                ResultMessage = "Книга успешно добавлена!";
            }
        }
    }
}
