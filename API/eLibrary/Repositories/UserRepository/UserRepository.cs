using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using eLibrary.Models;
using MySql.Data.MySqlClient;

namespace eLibrary.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDb _db;

        public UserRepository(AppDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText =
                @"SELECT * FROM `user`";

            var res = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return res;
        }

        public async Task<User> AddUser(User user)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `user` (`username`) VALUES (@username);";
            BindParams(cmd, user);

            await cmd.ExecuteNonQueryAsync();
            user.Id = (int) cmd.LastInsertedId;
            return user;
        }

        private static async Task<List<User>> ReadAllAsync(DbDataReader reader)
        {
            var users = new List<User>();
            await using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1)
                    };

                    users.Add(user);
                }
            }

            return users;
        }
        
        private static void BindParams(MySqlCommand cmd, User user)
        {
            cmd.Parameters.Add(new MySqlParameter("@username", user.Username));
        }
    }
}