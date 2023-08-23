using BookVisionWebApp.Models;

namespace BookVisionWebApp.DAL.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<Book> GetById(T entity);
        public Task Create(T entity);
        public Task Delete(T entity);
        public Task Update(T entity);
    }
}
