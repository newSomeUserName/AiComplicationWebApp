using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Domain.Entities.AI
{
    public class InputMessage
    {
        public string Content { get; set; } = null!;
        public MessageRole Role { get; set; } 
    }
}
