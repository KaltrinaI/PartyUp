using Microsoft.AspNetCore.Identity;

namespace PartyUp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<ReservationRequest> ReservationRequests { get; set; }
    }
}
