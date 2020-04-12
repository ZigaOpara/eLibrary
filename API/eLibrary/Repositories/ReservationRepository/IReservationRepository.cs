using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;

namespace eLibrary.Repositories.ReservationRepository
{
    public interface IReservationRepository
    {
        public Task<IEnumerable<Reservation>> GetReservations();
        public Task<IEnumerable<Reservation>> GetReservationsForUser(int id);
        public Task<Reservation> GetReservation(int id);
        public Task<Reservation> AddReservation(Reservation reservation);
        public Task<bool> RemoveReservation(int id);
    }
}