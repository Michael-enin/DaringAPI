using System;

namespace DaringAPI.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUserName { get; set; }
        public string SenderPhotoUrl { get; set; }
          //recipient
        public int RecipientId { get; set; }

        public string RecipientUserName { get; set; }
        public string RecipientPhotoUrl { get; set; }
           //datetime when recipient has seen the message
           public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime  MessageSentTime { get; set; }
    }
}