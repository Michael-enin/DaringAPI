using System;

namespace DaringAPI.Entities
{
    public class Message
    { 
           //sender
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUserName { get; set; }
        public AppUser Sender { get; set; }
          //recipient
        public int RecipientId { get; set; }

        public string RecipientUserName { get; set; }
        public AppUser Recipient { get; set; }
           //datetime when recipient has seen the message
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime  MessageSentTime { get; set; }=DateTime.Now;
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}