using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Core.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ChatName { get; set; } = null!;
        public string Model { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
