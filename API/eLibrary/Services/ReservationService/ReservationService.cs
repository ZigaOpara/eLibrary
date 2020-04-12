using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;
using eLibrary.Providers.BookProvider;
using eLibrary.Providers.ReservationProvider;

namespace eLibrary.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IBookProvider _bookProvider;

        public ReservationService(IReservationProvider reservationProvider, IBookProvider bookProvider)
        {
            _reservationProvider = reservationProvider;
            _bookProvider = bookProvider;
        }
        
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var res = await _reservationProvider.GetReservations();
            return res;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForUser(int id)
        {
            var res = await _reservationProvider.GetReservationsForUser(id);
            return res;
        }

        public async Task<Reservation> AddReservation(Reservation reservation)
        {
            var book = await _bookProvider.GetBook(reservation.BookId);
            if (book.Stock <= 0) throw new Exception("Book not in stock");
            book.Stock -= 1;
            var res = await _reservationProvider.AddReservation(reservation);
            await _bookProvider.EditBook(book);
            return res;
        }

        public async Task<bool> RemoveReservation(int id)
        {
            var reservation = await _reservationProvider.GetReservation(id);
            var book = await _bookProvider.GetBook(reservation.BookId);
            book.Stock += 1;
            var res = await _reservationProvider.RemoveReservation(id);
            await _bookProvider.EditBook(book);
            return res;
        }
    }
}