using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;
using eLibrary.Repositories;
using eLibrary.Repositories.BookRepository;

namespace eLibrary.Providers.BookProvider
{
    public class BookProvider : IBookProvider
    {
        // TODO: Add if/else to check if valid
        private readonly IBookRepository _bookRepository;

        public BookProvider(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.GetBooks();
        }

        public async Task<Book> GetBook(int id)
        {
            return await _bookRepository.GetBook(id);
        }

        public async Task<Book> AddBook(Book book)
        {
            return await _bookRepository.AddBook(book);
        }

        public async Task<Book> EditBook(Book book)
        {
            return await _bookRepository.EditBook(book);
        }

        public async Task<bool> DeleteBook(int id)
        {
            return await _bookRepository.DeleteBook(id);
        }
    }
}