using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Models.Messege
{
    public record SendMessegeCommand(Guid ChatId, string Message, bool IsUser);
    
}
