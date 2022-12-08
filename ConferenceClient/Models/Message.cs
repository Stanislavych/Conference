using System;

namespace ConferenceClient
{
    public class Message
    {
        public int MessageId { get; set; }
        public DateTime Date { get; set; }
        public string textMessage { get; set; } = string.Empty;
        public User user { get; set; }
        public int UserId { get; set; }
    }
}