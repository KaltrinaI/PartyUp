using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Services.Interfaces;

namespace PartyUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationRequestController : ControllerBase
    {
        private readonly IReservationRequestService _rrService;

        public ReservationRequestController(IReservationRequestService reservationRequestService)
        {
            _rrService = reservationRequestService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddReservationRequest(ReservationRequestDTO? reservationRequestDTO)
        {
            await _rrService.AddReservationRequest(reservationRequestDTO);
            return Ok();
        }

        [HttpDelete("{reservationRequestId}")]
        [Authorize]
        public async Task<ActionResult> DeleteReservationRequest(int reservationRequestId)
        {
            await _rrService.DeleteReservationRequest(reservationRequestId);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ReservationResponseDTO>>> GetAllReservationRequests()
        {
            var response = await _rrService.GetAllReservationRequests();
            return Ok(response);
        }

        [HttpGet("reservationsById/{reservationRequestId}")]
        [Authorize]
        public async Task<ActionResult<ReservationResponseDTO>> GetReservationRequestById(int reservationRequestId)
        {
            var response = await _rrService.GetReservationRequestById(reservationRequestId);
            return Ok(response);
        }

        [HttpGet("reservationsByStatus{status}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ReservationResponseDTO>>> GetReservationRequestsByStatus(ReservationStatus status)
        {
            var response = await _rrService.GetReservationRequestsByStatus(status);
            return Ok(response);
        }

        [HttpPut("{reservationRequestId}")]
        [Authorize]
        public async Task<ActionResult> UpdateReservationRequest(ReservationRequestDTO requestBody, int reservationRequestId)
        {
            await _rrService.UpdateReservationRequest(requestBody, reservationRequestId);
            return Ok();
        }
    }
}
