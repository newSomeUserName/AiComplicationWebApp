using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Models.Chat
{
    public record CreateChatCommand(Guid UserId, string? Model);
    
}
