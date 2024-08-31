using PartyUp.Models;

namespace PartyUp.DTOs
{
    public class ReservationResponseDTO
    {
        public int ReservationRequestId { get; set; }
        public EventRequestDTO? Event { get; set; }
        public ReservationStatus Status { get; set; }
        public int NrOfPeople { get; set; }
    }
}
