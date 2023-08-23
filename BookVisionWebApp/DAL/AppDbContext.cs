using BookVisionWebApp.DAL.Config;
using BookVisionWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookVisionWebApp.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("BooksVision");
            modelBuilder.ApplyConfiguration(new BookDbConfig());
        }
    }
}
