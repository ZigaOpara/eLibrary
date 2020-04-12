using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;

namespace eLibrary.Services.ReservationService
{
    public interface IReservationService
    {
        public Task<IEnumerable<Reservation>> GetReservations();
        public Task<IEnumerable<Reservation>> GetReservationsForUser(int id);
        public Task<Reservation> AddReservation(Reservation reservation);
        public Task<bool> RemoveReservation(int id);
    }
}