namespace PartyUp.Models
{
    public class ReservationRequest
    {
        public int ReservationRequestId { get; set; }
        public Event Event { get; set; }
        public ReservationStatus Status { get; set; }
        public int NrOfPeople { get; set; }
        public int EventId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }

    public enum ReservationStatus
    {
        Requested,
        Canceled,
        Confirmed,
        Declined
    }
}
