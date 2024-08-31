using PartyUp.Models;

namespace PartyUp.DTOs
{
    public class ReservationRequestDTO
    {
        public string UserId { get; set; }
        public int EventId { get; set; }
        public ReservationStatus Status { get; set; }
        public int NrOfPeople { get; set; }
    }
}
