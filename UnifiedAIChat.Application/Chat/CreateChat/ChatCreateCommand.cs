using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Chat.CreateChat
{
    public record CreateChatCommand(Guid UserId, string? Model);
    
}
