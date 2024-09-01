namespace PartyUp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeOfComment { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
