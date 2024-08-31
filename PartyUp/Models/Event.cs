using System.Collections.ObjectModel;

namespace PartyUp.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public BusinessEntity Organizer { get; set; }
        public int OrganizerId { get; set; }
        public string EventName { get; set; }
        public DateTime DateTimeOfEvent { get; set; }
        public string PosterUrl { get; set; }
        public double Price { get; set; }
        public int NumberOfReservations { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public List<ReservationRequest> ReservationRequests { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
   
}