namespace PartyUp.DTOs
{
    public class RegistrationRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public string Password { get; set; }

    }
}
