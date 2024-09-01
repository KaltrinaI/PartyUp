using System;

namespace PartyUp.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        
        public string Token { get; set; }
        public string Password { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}