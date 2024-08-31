using PartyUp.Models;

namespace PartyUp.DTOs
{
    public class BusinessEntityRequestDTO
    {
        public string OwnerEmail { get; set; }
        public string Name { get; set; }
        public string BusinessId { get; set; }
        public LocationDTO Location { get; set; }
        public List<EventRequestDTO> Events { get; set; }

    }
}
