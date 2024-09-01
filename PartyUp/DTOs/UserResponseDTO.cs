namespace PartyUp.DTOs
{
    public class UserResponseDTO
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
    }
}
