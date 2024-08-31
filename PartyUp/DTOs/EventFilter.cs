using PartyUp.Models;

namespace PartyUp.DTOs
{
    public class EventFilter
    {
        public string UserId { get; set; }
        public string Text {  get; set; }
        public List<Tag> Tags { get; set; }
        public string LocationAddress {  get; set; }

    }
}
