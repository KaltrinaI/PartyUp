using PartyUp.DTOs;
using PartyUp.Models;

namespace PartyUp.Services.Interfaces
{
    public interface IReservationRequestService
    {
        Task<ReservationResponseDTO> GetReservationRequestById(int reservationRequestId);
        Task<IEnumerable<ReservationResponseDTO>> GetAllReservationRequests();
        Task<IEnumerable<ReservationResponseDTO>> GetReservationRequestsByStatus(ReservationStatus status);
        Task AddReservationRequest(ReservationRequestDTO? reservationRequestDTO);
        Task UpdateReservationRequest(ReservationRequestDTO requestBody, int reservationRequestId);
        Task DeleteReservationRequest(int reservationRequestId);
    }
}
