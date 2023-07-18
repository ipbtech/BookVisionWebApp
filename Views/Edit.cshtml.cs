using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebApp.DAL;
using MyFirstWebApp.Domain;
using MyFirstWebApp.Services;

namespace MyFirstWebApp.Views
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext appDbContext;
        private readonly BookService bs;
        public string ResultMessage { get; private set; } = string.Empty;
        public string ErrorMessage { get; private set; } = string.Empty;
        public Book EditableBook { get; private set; }
        public long EditableBookId { get; private set; }
        public EditModel(AppDbContext _dbContext)
        {
            appDbContext = _dbContext;
            bs = new BookService(appDbContext);
        }

        public void OnGet(long id)
       {
            var data = PageContext.HttpContext.Request.Query;
            EditableBookId = Convert.ToInt64(data["id"].ToString());
            EditableBook = bs.GetById(EditableBookId);
        }
        public void OnPost(string title, string author, string description, string price)
        {
            var data = PageContext.HttpContext.Request.Query;
            EditableBookId = Convert.ToInt64(data["id"].ToString());
            EditableBook = bs.GetById(EditableBookId);

            double verifyprice;
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
            {
                ResultMessage = "Изменения не приняты!";
                ErrorMessage = "Название и автор не могут быть пустыми.";
            }
            else if (!double.TryParse(price, out verifyprice))
            {
                ResultMessage = "Изменения не приняты!";
                ErrorMessage = "Введенная стоимость некорректна.";
            }
            else
            {
                EditableBook.Title = title;
                EditableBook.Author = author;
                EditableBook.Description = description;
                EditableBook.Price = verifyprice;

                bs.UpdateEntity(EditableBook);
                bs.SaveChangesAsync();

                ResultMessage = "Книга успешно обновлена!";
            }
        }
    }
}
