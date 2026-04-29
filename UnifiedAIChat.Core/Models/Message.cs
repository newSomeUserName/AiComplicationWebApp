using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Core.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public string Content { get; set; } = null!;
        public MessageRole Role { get; set; }
        public int TokenUsed { get; set; }
        public DateTime CreatedAt { get; set; }
        public Chat Chat { get; set; } = null!;

    }
}
