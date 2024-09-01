using PartyUp.Models;

namespace PartyUp.DTOs
{
    public class EventResponseDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeOfEvent { get; set; }
        public LocationDTO Location { get; set; }
        public string PosterUrl { get; set; }
        public int EventTax { get; set; }
        public int NumberOfReservations { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
