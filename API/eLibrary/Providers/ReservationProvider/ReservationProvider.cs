using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;
using eLibrary.Repositories.ReservationRepository;

namespace eLibrary.Providers.ReservationProvider
{
    public class ReservationProvider : IReservationProvider
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationProvider(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var res = await _reservationRepository.GetReservations();
            return res;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForUser(int id)
        {
            var res = await _reservationRepository.GetReservationsForUser(id);
            return res;
        }
        
        public async Task<Reservation> GetReservation(int id)
        {
            var res = await _reservationRepository.GetReservation(id);
            return res;
        }

        public async Task<Reservation> AddReservation(Reservation reservation)
        {
            var res = await _reservationRepository.AddReservation(reservation);
            return res;
        }

        public async Task<bool> RemoveReservation(int id)
        {
            var res = await _reservationRepository.RemoveReservation(id);
            return res;
        }
    }
}