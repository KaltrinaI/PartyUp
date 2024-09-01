using System;

namespace PartyUp.Models
{
    public class Comment
    {
        public string Text { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public DateTime TimeOfComment { get; set; }

        public string DisplayTime => $"{TimeOfComment.ToLocalTime().ToShortDateString()}, {TimeOfComment.ToLocalTime().ToShortTimeString()}";
    }
}