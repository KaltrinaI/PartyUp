using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Repositories.Interfaces;
using PartyUp.Services.Interfaces;

namespace PartyUp.Services.Implementations
{
    public class ReservationRequestService : IReservationRequestService
    {
        private readonly IReservationRequestRepository _rrRepository;

        public ReservationRequestService(IReservationRequestRepository reservationRequestRepository)
        {
            _rrRepository = reservationRequestRepository;
        }
        public async Task AddReservationRequest(ReservationRequestDTO? reservationRequestDTO)
        {
            await _rrRepository.AddReservationRequest(reservationRequestDTO);
        }

        public async Task DeleteReservationRequest(int reservationRequestId)
        {
            await _rrRepository.DeleteReservationRequest(reservationRequestId);
        }

        public async Task<IEnumerable<ReservationResponseDTO>> GetAllReservationRequests()
        {
            return await _rrRepository.GetAllReservationRequests();
        }

        public async Task<ReservationResponseDTO> GetReservationRequestById(int reservationRequestId)
        {
            return await _rrRepository.GetReservationRequestById(reservationRequestId);
        }

        public async Task<IEnumerable<ReservationResponseDTO>> GetReservationRequestsByStatus(ReservationStatus status)
        {
            return await _rrRepository.GetReservationRequestsByStatus(status);
        }

        public async Task UpdateReservationRequest(ReservationRequestDTO requestBody, int reservationRequestId)
        {
            await _rrRepository.UpdateReservationRequest(requestBody, reservationRequestId);
        }
    }
}
