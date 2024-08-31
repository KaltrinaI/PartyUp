namespace PartyUp.DTOs
{
    public class UserRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
    }
}
