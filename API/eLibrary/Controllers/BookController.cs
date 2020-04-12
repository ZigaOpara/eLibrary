using System;
using System.Threading.Tasks;
using eLibrary.Models;
using eLibrary.Services.BookService;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            var book = await _bookService.GetBook(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var res = await _bookService.AddBook(book);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> EditBook([FromBody] Book book)
        {
            var res = await _bookService.EditBook(book);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var res = await _bookService.DeleteBook(id);
            return Ok(res);
        }

        [HttpPost("rating")]
        public async Task<IActionResult> AddRating(Rate rate)
        {
            var res = await _bookService.AddRating(rate);
            return Ok(res);
        }
    }
}