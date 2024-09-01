namespace PartyUp.DTOs
{
    public class CommentResponseDTO
    {
        public int CommentId { get; set; }
        public UserResponseDTO? User { get; set; }
        public string? UserId { get; set; }
        public int EventId { get; set; }
        public string Text { get; set; }
        public DateTime? TimeOfComment { get; set; }
    }
}
