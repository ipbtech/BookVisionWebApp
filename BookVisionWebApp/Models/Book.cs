using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookVisionWebApp.Models
{
    public class Book
    {  
        public long Id { get; set; }

        [DisplayName("Название книги:")]
        [Required(ErrorMessage = "Введите название книги")]
        [MaxLength(50, ErrorMessage = "Название книги должно быть не более 50 символов")]
        public string Title { get; set; }

        [DisplayName("Автор книги:")]
        [Required(ErrorMessage = "Введите автора книги")]
        [MinLength(2, ErrorMessage = "Имя автора книги должно быть не менее двух символов")]
        [MaxLength(50, ErrorMessage = "Название книги должно быть не более 50 символов")]
        public string Author { get; set; }

        [DisplayName("Стоимость книги:")]
        [Required(ErrorMessage = "Введите стоимость книги")]
        public double Price { get; set; }

        [DisplayName("Описание книги:")]
        [MaxLength(200, ErrorMessage = "Описание книги должно быть не более 200 символов")]
        public string? Description { get; set; }
    }

    public class BookEqualityComparer : IEqualityComparer<Book>
    {
        public bool Equals(Book? book1, Book? book2)
        {
            return book1.Title == book2.Title && book1.Author == book2.Author;
        }

        public int GetHashCode([DisallowNull] Book obj)
        {
            return HashCode.Combine<Book>(obj);
        }
    }
}