namespace PartyUp.DTOs
{
    public class CommentResponseDTO
    {
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public string Content { get; set; }
    }
}
