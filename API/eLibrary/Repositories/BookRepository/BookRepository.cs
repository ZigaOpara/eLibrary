using System;
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
            cmd.CommandText =
                @"SELECT `idbook`, `title`, `author`, AVG(`rating`), `stock` FROM `book` LEFT  JOIN `rating` ON `book`.`idbook`=`rating`.`book_id` GROUP BY `idbook`";
            
            var res = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return res;
        }

        public async Task<Book> GetBook(int id)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `idbook`, `title`, `author`, AVG(`rating`), `stock` FROM `book` LEFT JOIN `rating` ON `book`.`idbook`=`rating`.`book_id` WHERE `idbook` = @id";
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

        public async Task<Rate> AddRating(Rate rate)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `rating` (`rating`, `user_id`, `book_id`) VALUES (@rating, @userId, @bookId);";
            cmd.Parameters.Add(new MySqlParameter("@rating", rate.Rating));
            cmd.Parameters.Add(new MySqlParameter("@userId", rate.UserId));
            cmd.Parameters.Add(new MySqlParameter("@bookId", rate.BookId));

            await cmd.ExecuteNonQueryAsync();
            rate.Id = (int) cmd.LastInsertedId;
            return rate;
        }

        public async Task<Rate> EditRating(Rate rate)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText =
                @"UPDATE `rating` SET `rating` = @rating WHERE `idrating` = @id;";
            cmd.Parameters.Add(new MySqlParameter("@rating", rate.Rating));
            cmd.Parameters.Add(new MySqlParameter("@id", rate.Id));

            await cmd.ExecuteNonQueryAsync();
            return rate;
        }

        public async Task<List<Rate>> GetRatings()
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText =
                @"SELECT * FROM `rating`";
            
            var res = await ReadRatingsAsync(await cmd.ExecuteReaderAsync());
            return res;
        }

        private static async Task<List<Book>> ReadAllAsync(DbDataReader reader)
        {
            var books = new List<Book>();
            await using (reader)
            {
                while (await reader.ReadAsync())
                {
                    Book book;
                    if (reader.IsDBNull(3))
                    {
                        book = new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Stock = reader.GetInt32(4)
                        };  
                    }
                    else
                    {
                        book = new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Rating = decimal.ToDouble((decimal) reader.GetValue(3)),
                            Stock = reader.GetInt32(4)
                        };
                    }

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
        
        private static async Task<List<Rate>> ReadRatingsAsync(DbDataReader reader)
        {
            var ratings = new List<Rate>();
            await using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var rate = new Rate
                    {
                        Id = reader.GetInt32(0),
                        Rating = reader.GetInt32(1),
                        UserId = reader.GetInt32(2),
                        BookId = reader.GetInt32(3),
                    };

                    ratings.Add(rate);
                }
            }

            return ratings;
        }
    }
}