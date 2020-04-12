using System.Collections.Generic;
using System.Linq;
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
        
        public async Task<Rate> AddRating(Rate rate)
        {
            var ratings = await GetRatings();
            var existingRating =
                ratings.FirstOrDefault(rating => rating.UserId == rate.UserId && rating.BookId == rate.BookId);
            if (existingRating != null)
            {
                rate.Id = existingRating.Id;
                return await _bookRepository.EditRating(rate);
            }
            
            var res = await _bookRepository.AddRating(rate);
            return res;
        }

        private async Task<List<Rate>> GetRatings()
        {
            var res = await _bookRepository.GetRatings();
            return res;
        }
    }
}