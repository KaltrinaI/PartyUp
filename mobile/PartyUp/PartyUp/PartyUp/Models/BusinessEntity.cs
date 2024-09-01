using System.Collections.Generic;

namespace PartyUp.Models
{
    public class BusinessEntity
    {
        public User Owner { get; set; }
        public string Name { get; set; }
        public string BusinessId { get; set; }
        public string BusinessTaxNumber { get; set; }
        public Location Location { get; set; }
        public List<Event> Events { get; set; }
    }
}