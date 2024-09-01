namespace PartyUp.Models
{
    public enum ReservationStatus
    {
        Requested,
        Canceled,
        Approved,
        Declined
    }
    public class ReservationRequest
    {
        public Event Event { get; set; }
        public ReservationStatus Status { get; set; }
        public int NrOfPeople { get; set; }
    }
}