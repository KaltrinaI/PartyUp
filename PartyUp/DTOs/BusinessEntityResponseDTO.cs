namespace PartyUp.DTOs
{
    public class BusinessEntityResponseDTO
    {
        public int BusinessEntityId { get; set; }
        public string OwnerEmail { get; set; }
        public string Name { get; set; }
        public string BusinessId { get; set; }
        public LocationDTO Location { get; set; }
        public List<EventResponseDTO> Events { get; set; }
    }
}
