using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using eLibrary.Models;
using MySql.Data.MySqlClient;

namespace eLibrary.Repositories.ReservationRepository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDb _db;

        public ReservationRepository(AppDb db)
        {
            _db = db;
        }
        
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText =
                @"SELECT * FROM `reservation`";

            var res = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return res;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForUser(int id)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `reservation` WHERE `user_id` = @id";
            BindId(cmd, id);

            var res = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return res;
        }

        public async Task<Reservation> GetReservation(int id)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `reservation` WHERE `idreservation` = @id";
            BindId(cmd, id);

            var res = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return res.Count > 0 ? res[0] : null;
        }

        public async Task<Reservation> AddReservation(Reservation reservation)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `reservation` (`user_id`, `book_id`) VALUES (@userId, @bookId);";
            BindParams(cmd, reservation);

            await cmd.ExecuteNonQueryAsync();
            reservation.Id = (int) cmd.LastInsertedId;
            return reservation;
        }

        public async Task<bool> RemoveReservation(int id)
        {
            var cmd = _db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `reservation` WHERE `idreservation` = @id;";
            BindId(cmd, id);

            await cmd.ExecuteNonQueryAsync();
            return true;
        }
        
        private static async Task<List<Reservation>> ReadAllAsync(DbDataReader reader)
        {
            var reservations = new List<Reservation>();
            await using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var reservation = new Reservation
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        BookId = reader.GetInt32(2),
                    };

                    reservations.Add(reservation);
                }
            }

            return reservations;
        }
        
        private static void BindId(MySqlCommand cmd, int id)
        {
            cmd.Parameters.Add(new MySqlParameter("@id", id));
        }

        private static void BindParams(MySqlCommand cmd, Reservation reservation)
        {
            cmd.Parameters.Add(new MySqlParameter("@userId", reservation.UserId));
            cmd.Parameters.Add(new MySqlParameter("@bookId", reservation.BookId));
        }
    }
}