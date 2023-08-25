using System.Diagnostics.CodeAnalysis;

namespace BookVisionWebApp.Models
{
    public class Book
    {  
        public long Id { get; set; }      
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public Book(string title, string author, double price)
        {
            Title = title;
            Author = author;
            Price = price;
        }
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