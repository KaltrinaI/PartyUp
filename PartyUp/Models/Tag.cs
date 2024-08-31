using System.Text.Json.Serialization;

namespace PartyUp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        [JsonIgnore]
        public ICollection<Event>? Events { get; set; }
    }
}
