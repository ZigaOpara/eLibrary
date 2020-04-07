using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using eLibrary.Models;
using MySql.Data.MySqlClient;

namespace eLibrary.Repositories.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDb _db;

        public BookRepository(AppDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `book`";

            var res = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return res;
        }

        public async Task<Book> GetBook(int id)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `book` WHERE `idbook` = @id";
            BindId(cmd, id);

            var res = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return res.Count > 0 ? res[0] : null;
        }

        public async Task<Book> AddBook(Book book)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `book` (`title`, `author`, `stock`) VALUES (@title, @author, @stock);";
            BindParams(cmd, book);

            await cmd.ExecuteNonQueryAsync();
            book.Id = (int) cmd.LastInsertedId;
            return book;
        }

        public async Task<Book> EditBook(Book book)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText =
                @"UPDATE `book` SET `title` = @title, `author` = @author, `stock` = @stock WHERE `idbook` = @id;";
            BindParams(cmd, book);
            BindId(cmd, book.Id);
            
            await cmd.ExecuteNonQueryAsync();
            return book;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `book` WHERE `idbook` = @id;";
            BindId(cmd, id);

            await cmd.ExecuteNonQueryAsync();
            return true;
        }

        private static async Task<List<Book>> ReadAllAsync(DbDataReader reader)
        {
            var books = new List<Book>();
            await using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var book = new Book
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Author = reader.GetString(2),
                        Stock = reader.GetInt32(3)
                    };
                    books.Add(book);
                }
            }

            return books;
        }

        private static void BindId(MySqlCommand cmd, int id)
        {
            cmd.Parameters.Add(new MySqlParameter("@id", id));
        }

        private static void BindParams(MySqlCommand cmd, Book book)
        {
            cmd.Parameters.Add(new MySqlParameter("@title", book.Title));
            cmd.Parameters.Add(new MySqlParameter("@author", book.Author));
            cmd.Parameters.Add(new MySqlParameter("@stock", book.Stock));
        }
    }
}