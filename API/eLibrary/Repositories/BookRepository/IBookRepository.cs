using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;

namespace eLibrary.Repositories.BookRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(int id);
        Task<Book> AddBook(Book book);
        Task<Book> EditBook(Book book);
        Task<bool> DeleteBook(int id);
    }
}