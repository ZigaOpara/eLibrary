using System.Collections.Generic;
using System.Threading.Tasks;
using eLibrary.Models;
using eLibrary.Services.ReservationService;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservations()
        {
            var res = await _reservationService.GetReservations();
            return Ok(res);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationsForUser([FromRoute] int id)
        {
            var res = await _reservationService.GetReservationsForUser(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] Reservation reservation)
        {
            var res = await _reservationService.AddReservation(reservation);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveReservation([FromRoute] int id)
        {
            var res = await _reservationService.RemoveReservation(id);
            return Ok(res);
        }
    }
}