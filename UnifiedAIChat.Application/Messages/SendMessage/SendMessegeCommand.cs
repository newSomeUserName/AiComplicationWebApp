using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Messages.SendMessage
{
    public record SendMessegeCommand(Guid ChatId, string Message, bool IsUser);
    
}
