using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;
using eLibrary.Providers.BookProvider;

namespace eLibrary.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookProvider _bookProvider;

        public BookService(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }
        
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookProvider.GetBooks();
        }

        public async Task<Book> GetBook(int id)
        {
            return await _bookProvider.GetBook(id);
        }

        public async Task<Book> AddBook(Book book)
        {
            return await _bookProvider.AddBook(book);
        }

        public async Task<Book> EditBook(Book book)
        {
            return await _bookProvider.EditBook(book);
        }

        public async Task<bool> DeleteBook(int id)
        {
            return await _bookProvider.DeleteBook(id);
        }
    }
}